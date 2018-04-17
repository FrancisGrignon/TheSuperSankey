using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sankey.Domain.Models;

namespace Sankey.Infrastructure.EntityConfigurations
{
    public class TableEntityTypeConfiguration : IEntityTypeConfiguration<Table>
    {
        public void Configure(EntityTypeBuilder<Table> builder)
        {
            builder.ToTable("Table");

            builder.HasKey(ci => ci.Id);

            builder.Property(ci => ci.NameEn)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(ci => ci.NameFr)
                .IsRequired(true)
                .HasMaxLength(255);

            builder.Property(ci => ci.NoteEn)
                .IsRequired(true)
                .HasMaxLength(8192);

            builder.Property(ci => ci.NoteFr)
                .IsRequired(true)
                .HasMaxLength(8192);

            builder.Property(ci => ci.Tag)
                .IsRequired(true)
                .HasMaxLength(255);
                
            builder.Property(ci => ci.Id)
               .IsRequired();
        }
    }
}