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
        // Yalnız JWT Bearer scheme üçün konfiqurasiya et
        // name null ola bilər (default scheme) və ya JwtBearerDefaults.AuthenticationScheme olmalıdır
        if (name != null && name != JwtBearerDefaults.AuthenticationScheme) 
            return;

        options.SaveToken = true;

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

            ClockSkew = TimeSpan.FromMinutes(5)
        };

        // JWT Bearer Events - authentication failure-ləri düzgün handle etmək üçün
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                // Token yoxdursa və ya authorization uğursuz olduqda
                // HandleResponse çağırmaqla default challenge response-u əvəz edirik
                context.HandleResponse();
                
                // Response artıq yazılıbsa, üstündən yazma
                if (!context.Response.HasStarted)
                {
                    context.Response.StatusCode = 401;
                    context.Response.ContentType = "application/json";
                }
                
                return Task.CompletedTask;
            }
        };
    }

    public void Configure(JwtBearerOptions options)
        => Configure(JwtBearerDefaults.AuthenticationScheme, options);
}
