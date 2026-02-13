using System.Text;
using Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace API.Options;

public class ConfigureJwtBearerOptions
    : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwt;

    public ConfigureJwtBearerOptions(IOptions<JwtOptions> jwt)
    {
        _jwt = jwt.Value;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        if (name != JwtBearerDefaults.AuthenticationScheme) return;

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

            ClockSkew = TimeSpan.Zero
        };
    }

    public void Configure(JwtBearerOptions options)
        => Configure(JwtBearerDefaults.AuthenticationScheme, options);
}