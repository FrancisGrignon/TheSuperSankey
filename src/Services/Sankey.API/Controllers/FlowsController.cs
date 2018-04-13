using Microsoft.AspNetCore.Mvc;
using Sankey.API.ViewModels;
using System;
using System.Collections.Generic;

namespace Sankey.API.Controllers
{
    [Route("api/[controller]")]
    public class FlowsController : Controller
    {
        public FlowsController()
        {

        }

        // GET api/flows
        [HttpGet]
        public IEnumerable<FlowViewModel> Get()
        {
            return Get("Canada", DateTime.Now.Year);
        }

        // GET api/flows/ca-qc/2015/fr
        [HttpGet("{id}")]
        public IEnumerable<FlowViewModel> Get(string geo, int year, string language = "en", string tag="all")
        {
            return new FlowViewModel[] { new FlowViewModel() };
        }
    }
}
