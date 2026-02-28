namespace BadmintonParty.CourtManagement.Api.Models.Auth;

public class LoginRequest
{
    public string IdToken { get; set; } = string.Empty;
}

public class LoginResponse
{
    public string Token { get; set; } = string.Empty;
    public MemberDto User { get; set; } = null!;
}

public class MemberDto
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string LineUserId { get; set; } = string.Empty;
    public string PictureUrl { get; set; } = string.Empty;
}

public class LineVerifyResponse
{
    public string iss { get; set; } = string.Empty;
    public string sub { get; set; } = string.Empty;
    public string aud { get; set; } = string.Empty;
    public long exp { get; set; }
    public long iat { get; set; }
    public string name { get; set; } = string.Empty;
    public string picture { get; set; } = string.Empty;
    public string email { get; set; } = string.Empty;
    public string error { get; set; } = string.Empty;
    public string error_description { get; set; } = string.Empty;
}
