using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class PropertyMediaConfiguration : IEntityTypeConfiguration<PropertyMedia>
{
    public void Configure(EntityTypeBuilder<PropertyMedia> builder)
    {
        builder.ToTable(nameof(PropertyMedia));

        builder.HasKey(pm => pm.Id);

        builder.Property(pm => pm.ObjectKey)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(pm => pm.Order)
            .IsRequired();

        builder.Property(pm => pm.PropertyAdId)
            .IsRequired();

        builder.HasOne(pm => pm.PropertyAd)
            .WithMany(pa => pa.PropertyMedias)
            .HasForeignKey(pm => pm.PropertyAdId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

