// 1️⃣ FAYL: src/Infrastructure/services/JwtTokenGenerator.cs
namespace Infrastructure.Services;

using Application.Abstracts.Services;
using Application.Options;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtOptions _options;
    private readonly ILogger<JwtTokenGenerator> _logger;

    public JwtTokenGenerator(
        IOptions<JwtOptions> options,
        ILogger<JwtTokenGenerator> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public string GenerateToken(User user)
    {
        _logger.LogInformation("🔐 Generating JWT Token for user: {UserId}", user.Id);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? ""),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new("fullName", user.FullName ?? "")
        };

        _logger.LogInformation("   Claims added:");
        foreach (var claim in claims)
        {
            _logger.LogInformation("      {Type} = {Value}", claim.Type, claim.Value);
        }

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_options.Secret));

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(_options.ExpirationMinutes);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        _logger.LogInformation("   Token generated:");
        _logger.LogInformation("      Issuer: {Issuer}", _options.Issuer);
        _logger.LogInformation("      Audience: {Audience}", _options.Audience);
        _logger.LogInformation("      Expires: {Expires} UTC", expiresAt);
        _logger.LogInformation("      Token (first 50 chars): {Token}...", tokenString[..Math.Min(50, tokenString.Length)]);

        return tokenString;
    }
}

