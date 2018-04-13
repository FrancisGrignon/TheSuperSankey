using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Sankey.API.ViewModels;
using Sankey.Infrastructure;
using System;
using System.Collections.Generic;
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

            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        // GET api/flows
        [HttpGet]
        public async Task<FlowViewModel> Get()
        {
            return await Get("ca", DateTime.Now.Year);
        }

        // GET api/flows/ca-qc/2015/all/fr
        [HttpGet("{geo}/{year:int}/{tag?}/{language?}")]
        public async Task<FlowViewModel> Get(string geo, int year, string tag="root", string language = "en")
        {
            var location = await _context.Geos.Where(p => p.Iso3166.Equals(geo)).SingleOrDefaultAsync();
            var flows = await _context.Flows.Include(p => p.Source).Include(p => p.Target).Where(p => p.GeoId.Equals(location.Id) && p.Year.Equals(year) && p.Tag.Equals(tag)).ToArrayAsync();
            var model = new FlowViewModel
            {
                Geo = location.Name(language),
                Year = year,
                Data = flows.Select(p => new FlowItemViewModel { Source = p.Source.Name(language), SourceId = p.Source.Id, Target = p.Target.Name(language), TargetId = p.TargetId, Value = p.Value }).ToArray()
            };

            return model;
        }
    }
}
