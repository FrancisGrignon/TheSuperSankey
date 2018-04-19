using System.Collections.Generic;

namespace Sankey.API.ViewModels
{
    public class D3ViewModel
    {
        public string Geography { get; set; }
        public string Name { get; set; }
        public string Note { get; set; }      
        public string Tag { get; set; }
        public int Year { get; set; }
        public D3SankeyViewModel Data { get; set; }
    }
}
