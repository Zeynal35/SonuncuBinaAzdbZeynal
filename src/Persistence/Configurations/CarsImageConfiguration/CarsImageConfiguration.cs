using Domain.Entities.Simple;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations.CarsImageConfiguration;

public class CarsImageConfiguration : IEntityTypeConfiguration<CarsImage>
{
    public void Configure(EntityTypeBuilder<CarsImage> builder)
    {
        builder.ToTable(nameof(CarsImage));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.FileName)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(x => x.FilePath)
               .IsRequired()
               .HasMaxLength(500);

        builder.Property(x => x.CreatedDate)
               .IsRequired();

        builder.Property(x => x.ModifiedDate)
               .IsRequired(false);
    }

}
