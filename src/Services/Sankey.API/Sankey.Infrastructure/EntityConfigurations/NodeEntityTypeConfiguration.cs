using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sankey.Domain.Models;

namespace Sankey.Infrastructure.EntityConfigurations
{
    public class NodeEntityTypeConfiguration : IEntityTypeConfiguration<Node>
    {
        public void Configure(EntityTypeBuilder<Node> builder)
        {
            builder.ToTable("Node");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.NameEn)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(ci => ci.NameFr)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(ci => ci.Id)
               .IsRequired();
        }
    }
}