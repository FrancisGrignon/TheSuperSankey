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

            builder.HasOne(ci => ci.Source)
                .WithMany()
                .HasForeignKey(ci => ci.SourceId);

            builder.HasOne(ci => ci.Geo)
                .WithMany()
                .HasForeignKey(ci => ci.GeoId);

            builder.HasOne(ci => ci.Target)
                .WithMany()
                .HasForeignKey(ci => ci.TargetId);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.Value)
                .IsRequired();

            builder.Property(ci => ci.Year)
                .IsRequired();

            builder.Property(ci => ci.Tag)
                .IsRequired(true)
                .HasMaxLength(255);
        }
    }
}