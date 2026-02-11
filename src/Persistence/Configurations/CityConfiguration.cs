using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.ToTable("Cities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(x => x.Area)
               .IsRequired();

        builder.Property(x => x.Population)
               .IsRequired();

        builder.Property(x => x.IsCapital)
               .IsRequired();

        builder.Property(x => x.IsTouristic)
               .IsRequired();

        builder.Property(x => x.Description)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.ImageUrl)
               .HasMaxLength(300);

        builder.Property(x => x.FlagUrl)
               .HasMaxLength(300);

        builder.Property(x => x.Elevation)
               .IsRequired();

        builder.Property(x => x.GDP)
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Density)
               .IsRequired();

        builder.Property(x => x.TransportSystem)
               .HasMaxLength(200);

        builder.Property(x => x.AirportsCount)
               .IsRequired();

        builder.Property(x => x.UniversitiesCount)
               .IsRequired();

        builder.Property(x => x.HospitalsCount)
               .IsRequired();

        builder.Property(x => x.CreatedDate)
               .IsRequired();

        builder.Property(x => x.ModifiedDate)
               .IsRequired(false);
        builder.HasIndex(x=>x.Name).IsUnique();
        builder.HasIndex(x=>x.IsCapital).IsUnique();
        builder.HasMany(x => x.Streets)
               .WithOne(x => x.City)
               .HasForeignKey(x => x.CityId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
