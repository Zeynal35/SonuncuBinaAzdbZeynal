using Application.Abstracts.Repositories;
using Application.Abstracts.Repositories.SimpleRepo;
using Application.Abstracts.Services;
using Application.Abstracts.Services.Simple;
using Application.Mapping;
using Application.Options;
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
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // ===================== DbContext =====================
        services.AddDbContext<BinaLiteDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // ===================== Identity =====================
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

        // ===================== JWT =====================
        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer();

        services.ConfigureOptions<ConfigureJwtBearerOptions>();

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAuthService, AuthService>();

        // ===================== Validators =====================
        services.AddValidatorsFromAssemblyContaining<CreatePropertyAdRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCityRequestValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateStreetRequestValidator>();

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
            options.MultipartBodyLengthLimit = 50 * 1024 * 1024; // 50MB
        });
    }
}
