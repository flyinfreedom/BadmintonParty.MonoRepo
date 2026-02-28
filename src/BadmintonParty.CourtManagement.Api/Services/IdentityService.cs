using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BadmintonParty.CourtManagement.Api.Models.Auth;
using BadmintonParty.Domain.AggregatesModel.MemberAggregate;
using Microsoft.IdentityModel.Tokens;

namespace BadmintonParty.CourtManagement.Api.Services;

public interface IIdentityService
{
    Task<LoginResponse?> LoginAsync(string idToken);
}

public class IdentityService : IIdentityService
{
    private readonly IConfiguration _configuration;
    private readonly IMemberRepository _memberRepository;
    private readonly HttpClient _httpClient;

    public IdentityService(
        IConfiguration configuration,
        IMemberRepository memberRepository,
        HttpClient httpClient)
    {
        _configuration = configuration;
        _memberRepository = memberRepository;
        _httpClient = httpClient;
    }

    public async Task<LoginResponse?> LoginAsync(string idToken)
    {
        var lineUser = await VerifyLineTokenAsync(idToken);
        if (lineUser == null) return null;

        var member = await _memberRepository.GetByLineUserIdAsync(lineUser.sub);
        if (member == null)
        {
            member = new Member
            {
                LineUserId = lineUser.sub,
                Name = lineUser.name,
                PictureUrl = lineUser.picture,
                Status = AccountStatus.Active, // Assuming this is valid
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            member = await _memberRepository.AddAsync(member);
        }
        else
        {
            // Update profile if changed
            bool updated = false;
            if (member.Name != lineUser.name) { member.Name = lineUser.name; updated = true; }
            if (member.PictureUrl != lineUser.picture) { member.PictureUrl = lineUser.picture; updated = true; }
            
            if (updated)
            {
                member.UpdatedAt = DateTime.UtcNow;
                await _memberRepository.UpdateAsync(member);
            }
        }

        var token = GenerateJwtToken(member);

        return new LoginResponse
        {
            Token = token,
            User = new MemberDto
            {
                Id = member.Id,
                Name = member.Name,
                LineUserId = member.LineUserId,
                PictureUrl = member.PictureUrl
            }
        };
    }

    private async Task<LineVerifyResponse?> VerifyLineTokenAsync(string idToken)
    {
        var clientId = _configuration["Line:ClientId"];
        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("id_token", idToken),
            new KeyValuePair<string, string>("client_id", clientId!)
        });

        var response = await _httpClient.PostAsync("https://api.line.me/oauth2/v2.1/verify", content);
        if (!response.IsSuccessStatusCode) return null;

        var result = await response.Content.ReadFromJsonAsync<LineVerifyResponse>();
        if (result == null) return null;

        // 確認：issuer 正確 (https://access.line.me)
        if (result.iss != "https://access.line.me") return null;

        // 確認：audience 是你 (你的 Channel ID)
        if (result.aud != clientId) return null;

        // 確認：沒過期 (LINE API 驗證時已經會處理過期，但這裡也可以補檢查)
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (result.exp < now) return null;

        return result;
    }

    private string GenerateJwtToken(Member member)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]!);

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, member.Id.ToString()),
                new Claim(ClaimTypes.Name, member.Name),
                new Claim("line_id", member.LineUserId)
            }),
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"]!)),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
