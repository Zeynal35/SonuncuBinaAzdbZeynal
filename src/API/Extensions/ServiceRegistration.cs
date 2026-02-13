using Application.Abstracts.Repositories;
using Application.Abstracts.Repositories.SimpleRepo;
using Application.Abstracts.Services;
using Application.Abstracts.Services.Simple;
using Application.Mapping;
using Application.Options;
using Application.Validations.Auth;
using Application.Validations.City;
using Application.Validations.PropertyAd;
using Application.Validations.Street;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Repositories;
using Persistence.Repositories.Simple;
using Persistence.Services;
using Infrastructure.Extensions;
using Infrastructure.Services;
using Domain.Entities;
using API.Options;

namespace API.Extensions;

public static class ServiceRegistration
{
    public static void AddMyServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ===================== MVC & Swagger =====================
        services.AddControllers();
        services.AddAuthorization();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
            {
                Title = "BinaAz API",
                Version = "v1",
                Description = "Daşınmaz əmlak elanları API"
            });

            options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                Description = "JWT Authorization header. Məsələn: Bearer {token}"
            });

            options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
            {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Reference = new Microsoft.OpenApi.Models.OpenApiReference
                        {
                            Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        // ===================== DbContext =====================
        services.AddDbContext<BinaLiteDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // ===================== JWT - AddIdentity-den EVVEL yazilir =====================
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer();

        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        // ===================== Identity - JWT-den SONRA yazilir =====================
        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<BinaLiteDbContext>()
        .AddDefaultTokenProviders();

        // AddIdentity-nin cookie redirect-ini söndür - API-da lazım deyil
        services.ConfigureApplicationCookie(options =>
        {
            options.Events.OnRedirectToLogin = context =>
            {
                context.Response.StatusCode = 401;
                return Task.CompletedTask;
            };
            options.Events.OnRedirectToAccessDenied = context =>
            {
                context.Response.StatusCode = 403;
                return Task.CompletedTask;
            };
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();

        // ===================== Refresh Token =====================
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();

        // ===================== Validators =====================
        services.AddValidatorsFromAssemblyContaining<CreatePropertyAdRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCityRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateStreetRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

        // ===================== AutoMapper =====================
        services.AddAutoMapper(cfg => { },
            typeof(PropertyAdProfile).Assembly,
            typeof(CityProfile).Assembly);

        // ===================== Repositories =====================
        services.AddScoped(typeof(IRepository<,>), typeof(GenericRepository<,>));

        // ===================== Services =====================
        services.AddScoped<IPropertyAdService, PropertyAdServices>();
        services.AddScoped<ICityServices, CityService>();
        services.AddScoped<IStreetService, StreetService>();
        services.AddScoped<ICarsImageRepo, CarsImageRepository>();
        services.AddScoped<ICarsImageService, CarsImageService>();
        services.AddScoped<IPropertyMediaService, PropertyMediaService>();

        // ===================== MinIO =====================
        services.AddMinioStorage(configuration);
        services.AddScoped<IFileStorageService, S3MinioFileStorageService>();

        // ===================== Form Options =====================
        services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 50 * 1024 * 1024;
        });
    }
}