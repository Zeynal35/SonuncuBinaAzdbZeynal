using Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace API.Options;

public class ConfigureJwtBearerOptions
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwt;
    private readonly ILogger<ConfigureJwtBearerOptions> _logger;

    public ConfigureJwtBearerOptions(
        IOptions<JwtOptions> jwt,
        ILogger<ConfigureJwtBearerOptions> logger)
    {
        _jwt = jwt.Value;
        _logger = logger;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme) return;

        _logger.LogInformation("🔧 Configuring JWT Bearer with:");
        _logger.LogInformation("   Issuer: {Issuer}", _jwt.Issuer);
        _logger.LogInformation("   Audience: {Audience}", _jwt.Audience);
        _logger.LogInformation("   Secret length: {Length} chars", _jwt.Secret.Length);

        // ✅ KRITIK: SaveToken = true
        options.SaveToken = true;

        // ✅ KRITIK: RequireHttpsMetadata = false (development üçün)
        options.RequireHttpsMetadata = false;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = _jwt.Issuer,
            ValidAudience = _jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Secret)),

            ClockSkew = TimeSpan.Zero,

            // ✅ Name və Role claim-lərini map edirik
            NameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        };

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                _logger.LogError("❌ JWT Authentication FAILED!");
                _logger.LogError("   Exception: {Message}", context.Exception.Message);
                _logger.LogError("   Exception Type: {Type}", context.Exception.GetType().Name);

                if (context.Exception.InnerException != null)
                {
                    _logger.LogError("   Inner Exception: {Inner}", context.Exception.InnerException.Message);
                }

                return Task.CompletedTask;
            },

            OnTokenValidated = context =>
            {
                _logger.LogInformation("✅ JWT Token VALIDATED successfully!");

                var userEmail = context.Principal?.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
                var userId = context.Principal?.FindFirst("sub")?.Value;

                _logger.LogInformation("   User ID: {UserId}", userId ?? "N/A");
                _logger.LogInformation("   User Email: {Email}", userEmail ?? "N/A");

                return Task.CompletedTask;
            },

            OnMessageReceived = context =>
            {
                var authHeader = context.Request.Headers["Authorization"].ToString();

                if (string.IsNullOrEmpty(authHeader))
                {
                    _logger.LogWarning("⚠️ No Authorization header found!");
                    return Task.CompletedTask;
                }

                _logger.LogInformation("📩 Authorization header received:");
                _logger.LogInformation("   Full header: {Header}", authHeader);

                if (authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                {
                    var token = authHeader.Substring("Bearer ".Length).Trim();
                    _logger.LogInformation("   Token (first 30 chars): {Token}...", token.Length > 30 ? token[..30] : token);
                    _logger.LogInformation("   Token length: {Length} chars", token.Length);
                }
                else
                {
                    _logger.LogWarning("   ⚠️ Header does NOT start with 'Bearer '");
                }

                return Task.CompletedTask;
            },

            OnChallenge = context =>
            {
                _logger.LogWarning("⚠️ JWT Authorization CHALLENGE!");
                _logger.LogWarning("   Error: {Error}", context.Error ?? "N/A");
                _logger.LogWarning("   Error Description: {Desc}", context.ErrorDescription ?? "N/A");
                _logger.LogWarning("   Request Path: {Path}", context.Request.Path);

                return Task.CompletedTask;
            }
        };
    }

    public void Configure(JwtBearerOptions options)
        => Configure(JwtBearerDefaults.AuthenticationScheme, options);
}