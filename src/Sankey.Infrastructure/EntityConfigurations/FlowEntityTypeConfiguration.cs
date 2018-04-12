using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sankey.Domain.Models;

namespace Sankey.Infrastructure.EntityConfigurations
{
    public class FlowEntityTypeConfiguration : IEntityTypeConfiguration<Flow>
    {
        public void Configure(EntityTypeBuilder<Flow> builder)
        {
            builder.ToTable("Flow");

            builder.HasKey(ci => ci.Id);

            builder.HasOne(ci => ci.Fuel)
                .WithMany()
                .HasForeignKey(ci => ci.FuelId);

            builder.HasOne(ci => ci.Geo)
                .WithMany()
                .HasForeignKey(ci => ci.GeoId);

            builder.HasOne(ci => ci.Supply)
                .WithMany()
                .HasForeignKey(ci => ci.SupplyId);

            builder.Property(ci => ci.Id)
               .IsRequired();

            builder.Property(ci => ci.Value)
               .IsRequired();

            builder.Property(ci => ci.Year)
               .IsRequired();
        }
    }
}