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
                .HasForeignKey(ci => ci.SourceId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Geo)
                .WithMany()
                .HasForeignKey(ci => ci.GeoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Target)
                .WithMany()
                .HasForeignKey(ci => ci.TargetId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ci => ci.Table)
                .WithMany()
                .HasForeignKey(ci => ci.TableId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(ci => ci.Id)
                .IsRequired();

            builder.Property(ci => ci.Value)
                .IsRequired();

            builder.Property(ci => ci.Year)
                .IsRequired();
        }
    }
}