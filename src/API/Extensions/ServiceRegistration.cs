using API.Options;
using Application.Abstracts.Repositories;
using Application.Abstracts.Repositories.SimpleRepo;
using Application.Abstracts.Services;
using Application.Abstracts.Services.Simple;
using Application.Options;
using Domain.Constants;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.Simple;
using Persistence.Services;

namespace API.Extensions;

public static class ServiceRegistration
{
    public static void AddMyServices(this IServiceCollection services, IConfiguration configuration)
    {
        // ===================== Controllers & Swagger =====================
        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "BinaAz API",
                Version = "v1"
            });

            // Swagger JWT
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Bearer {token}",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            };

            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new List<string>() }
            });
        });

        // ===================== Options =====================
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));
        services.Configure<SeedOptions>(configuration.GetSection(SeedOptions.SectionName));

        // ===================== DB =====================
        services.AddDbContext<BinaLiteDbContext>(opt =>
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // ===================== Identity =====================
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<BinaLiteDbContext>()
        .AddDefaultTokenProviders();

        // ===================== JWT Auth =====================
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        // ===================== Authorization (Policies) =====================
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policies.ManageCities, p =>
                p.RequireRole(RoleNames.Admin));

            options.AddPolicy(Policies.ManageProperties, p =>
                p.RequireAuthenticatedUser());
        });

        // ===================== DI (Repositories/Services) =====================
        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        // səndə SimpleRepo-lar var idi
        services.AddScoped(typeof(ISimpleRepository<>), typeof(SimpleRepository<>));

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        // Infrastructure-lar (məs: MinIO varsa)
        services.AddInfrastructure(configuration);

        // ===================== FluentValidation =====================
        services.AddValidatorsFromAssembly(AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "Application"));

        // ===================== File Upload Limit =====================
        services.Configure<FormOptions>(opt =>
        {
            opt.MultipartBodyLengthLimit = 100 * 1024 * 1024;
        });
    }
}

