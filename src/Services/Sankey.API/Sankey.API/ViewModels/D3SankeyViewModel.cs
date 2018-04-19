using System.Collections.Generic;
using Newtonsoft.Json;

[JsonObject("sankey")]
public class D3SankeyViewModel
{
    [JsonProperty("nodes")]
    public IEnumerable<D3NodeViewModel> Nodes { get; set; }
    
    [JsonProperty("links")]
    public IEnumerable<D3LinkViewModel> Links { get; set; }
}