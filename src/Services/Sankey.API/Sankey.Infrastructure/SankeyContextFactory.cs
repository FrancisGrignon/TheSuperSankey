using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sankey.Infrastructure
{
    public class SankeyContextFactory : IDesignTimeDbContextFactory<SankeyContext>
    {
        public SankeyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SankeyContext>();

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TheSuperSankey;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new SankeyContext(optionsBuilder.Options);
        }
    }
}
