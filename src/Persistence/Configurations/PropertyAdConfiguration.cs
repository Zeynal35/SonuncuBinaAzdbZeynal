using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PropertyAdConfiguration : IEntityTypeConfiguration<PropertyAd>
{
    public void Configure(EntityTypeBuilder<PropertyAd> builder)
    {
        builder.ToTable(nameof(PropertyAd));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.Location)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.RoomCount)
            .IsRequired();

        builder.Property(p => p.Area)
            .IsRequired()
            .HasPrecision(10, 2);

        builder.Property(p => p.IsFurnished)
            .IsRequired();

        builder.Property(p => p.IsMortgage)
            .IsRequired();

        builder.Property(p => p.IsExtract)
            .IsRequired();

        builder.Property(p => p.OfferType)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(p => p.RealEstateType)
            .IsRequired()
            .HasConversion<int>();

        builder.HasMany(p => p.PropertyMedias)
            .WithOne(pm => pm.PropertyAd)
            .HasForeignKey(pm => pm.PropertyAdId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
