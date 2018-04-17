using Microsoft.EntityFrameworkCore;
using Sankey.Domain.Models;
using Sankey.Infrastructure.EntityConfigurations;

namespace Sankey.Infrastructure
{
    public class SankeyContext: DbContext
    {
        public SankeyContext(DbContextOptions<SankeyContext> options) : base(options)
        {
            // Empty
        }

        public DbSet<Flow> Flows { get; set; }
        public DbSet<Geo> Geos { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<Table> Tables { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FlowEntityTypeConfiguration());
            builder.ApplyConfiguration(new GeoEntityTypeConfiguration());
            builder.ApplyConfiguration(new NodeEntityTypeConfiguration());
            builder.ApplyConfiguration(new TableEntityTypeConfiguration());
        }
    }
}
