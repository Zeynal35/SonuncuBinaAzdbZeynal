using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class StreetConfiguration : IEntityTypeConfiguration<Street>
{
    public void Configure(EntityTypeBuilder<Street> builder)
    {
        builder.ToTable("Streets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(100);
        builder.HasIndex(x => x.Name).IsUnique();

        builder.Property(x => x.Length)
               .IsRequired();

        builder.Property(x => x.Description)
               .HasMaxLength(300);

        builder.HasOne(x => x.City)
               .WithMany(x => x.Streets)
               .HasForeignKey(x => x.CityId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);
    }
}
