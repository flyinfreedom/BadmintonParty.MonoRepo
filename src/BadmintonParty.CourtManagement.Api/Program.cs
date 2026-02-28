using System.Text;
using BadmintonParty.CourtManagement.Api.Models.Auth;
using BadmintonParty.CourtManagement.Api.Services;
using BadmintonParty.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();

// Infrastructure
builder.Services.AddInfrastructure(builder.Configuration);

// Auth Services
builder.Services.AddHttpClient();
builder.Services.AddScoped<IIdentityService, IdentityService>();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? "A_Very_Long_And_Secure_Secret_Key_For_BadmintonParty_2024");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Set to true in production
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidateAudience = true,
        ValidAudience = jwtSettings["Audience"],
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy => policy.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.UseAuthentication();
app.UseAuthorization();

// Minimal API Endpoints
var authGroup = app.MapGroup("/api/auth");

authGroup.MapPost("/login", async ([FromBody] LoginRequest request, IIdentityService identityService) =>
{
    if (string.IsNullOrEmpty(request.IdToken))
    {
        return Results.BadRequest("IdToken is required.");
    }

    var response = await identityService.LoginAsync(request.IdToken);
    if (response == null)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(response);
})
.WithName("Login");

authGroup.MapPost("/verify", async ([FromBody] LoginRequest request, IIdentityService identityService) =>
{
    if (string.IsNullOrEmpty(request.IdToken))
    {
        return Results.BadRequest("IdToken is required.");
    }

    var response = await identityService.LoginAsync(request.IdToken);
    if (response == null)
    {
        return Results.Unauthorized();
    }

    return Results.Ok(new { Valid = true, User = response.User });
})
.WithName("Verify");

app.Run();
