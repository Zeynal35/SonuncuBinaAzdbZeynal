using API.Options;
using Application.Abstracts.Repositories;
using Application.Abstracts.Services;
using Application.Options;
using Domain.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Services;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // =========================
        // Identity
        // =========================
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<BinaLiteDbContext>()
        .AddDefaultTokenProviders();

        // =========================
        // Jwt Options Bind
        // =========================
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        // =========================
        // JWT Authentication
        // =========================
       services
       .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
       .AddJwtBearer();
        services.ConfigureOptions<ConfigureJwtBearerOptions>();


        // =========================
        // Swagger JWT Support
        // =========================
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header istifadə edin. Məsələn: Bearer {token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // =========================
        // Services
        // =========================
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}

