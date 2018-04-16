using System.Collections.Generic;

namespace Sankey.API.ViewModels
{
    public class FlowViewModel
    {
        public string Geo { get; set; }
        public string Tag { get; set; }        
        public int Year { get; set; }
        public IEnumerable<object[]> Data { get; set; }
    }
}
