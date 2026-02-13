using Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public sealed class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly JwtOptions _jwtOptions;

    public ConfigureJwtBearerOptions(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        // Default scheme üçün də işləsin
        Configure(options);
    }

    public void Configure(JwtBearerOptions options)
    {
        if (string.IsNullOrWhiteSpace(_jwtOptions.Secret))
            throw new InvalidOperationException("JWT Secret is missing in configuration section 'Jwt'.");

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = _jwtOptions.Issuer,

            ValidateAudience = true,
            ValidAudience = _jwtOptions.Audience,

            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwtOptions.Secret)
            )
        };
    }
}