using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sankey.API.ViewModels;
using Sankey.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Sankey.API.Controllers
{
    [Route("api/[controller]")]
    public class FlowsController : Controller
    {
        private readonly SankeyContext _context;
        private readonly SankeySettings _settings;

        public FlowsController(SankeyContext context, IOptionsSnapshot<SankeySettings> settings)
        {
            _context = context;
            _settings = settings.Value;

            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET api/flows
        [HttpGet]
        public async Task<FlowViewModel> Get()
        {
            return await Get("ca", "root", "ca", 2015);
        }

        // GET api/flows/en/root/ca-qc/2015
        [HttpGet("{language}/{tag}/{geo}/{year:int}")]
        public async Task<FlowViewModel> Get(string language, string tag, string geo, int year)
        {
            if (false == "fr".Equals(language))
            {
                language = "en";
            }

            if (true == string.IsNullOrEmpty(geo))
            {
                geo = "CA";
            }
            else
            {
                geo = geo.ToUpper();
            }

            var patate = _context.Geos.ToArray();
            var frite = _context.Nodes.ToArray();
            var graisseuse = _context.Flows.ToArray();

            var location = await _context.Geos.Where(p => p.Iso3166 == geo).SingleOrDefaultAsync();
            var flows = await _context.Flows.Include(p => p.Source).Include(p => p.Target).Where(p => p.Geo.Iso3166 == geo && p.Year == year && p.Tag == tag).ToArrayAsync();
            var model = new FlowViewModel
            {
                Geo = location?.Name(language),
                Tag = tag,
                Year = year,
                Data = flows.Select(p => new object[3] { p.Source.Name(language), p.Target.Name(language), p.Value })
            };

            return model;
        }
    }
}
