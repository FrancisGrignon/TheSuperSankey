using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sankey.Domain.Models;

namespace Sankey.Infrastructure.EntityConfigurations
{
    public class GeoEntityTypeConfiguration : IEntityTypeConfiguration<Geo>
    {
        public void Configure(EntityTypeBuilder<Geo> builder)
        {
            builder.ToTable("Fuel");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.NameEn)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(ci => ci.NameFr)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(ci => ci.Iso3166)
                .IsRequired(true)
                .HasMaxLength(5);

            builder.Property(ci => ci.Id)
               .IsRequired();
        }
    }
}