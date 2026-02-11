using Domain.Entities;
using Domain.Entities.Simple;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Context;

public class BinaLiteDbContext : IdentityDbContext<User>
{
    public BinaLiteDbContext(DbContextOptions<BinaLiteDbContext> options)
        : base(options)
    {
    }

    public DbSet<PropertyAd> PropertyAds { get; set; } = null!;
    public DbSet<PropertyMedia> PropertyMedias { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Street> Streets { get; set; } = null!;
    public DbSet<CarsImage> CarsImages { get; set; } = null!;

    // 🔥 RefreshToken üçün DbSet
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Bütün configuration class-ları avtomatik tətbiq edir
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(BinaLiteDbContext).Assembly);
    }
}

