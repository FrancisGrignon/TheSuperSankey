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
        public DbSet<Fuel> Fuels { get; set; }
        public DbSet<Geo> Geos { get; set; }
        public DbSet<Supply> Supplies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FlowEntityTypeConfiguration());
            builder.ApplyConfiguration(new FuelEntityTypeConfiguration());
            builder.ApplyConfiguration(new GeoEntityTypeConfiguration());
            builder.ApplyConfiguration(new SupplyEntityTypeConfiguration());
        }
    }
}
