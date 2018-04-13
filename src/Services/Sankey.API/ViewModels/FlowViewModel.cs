using System.Collections.Generic;

namespace Sankey.API.ViewModels
{
    public class FlowViewModel
    {
        public string Geo { get; set; }

        public int Year { get; set; }

        public FlowItemViewModel[] Data { get; set; }
    }
}
