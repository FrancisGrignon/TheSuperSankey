using Newtonsoft.Json;

[JsonObject("Node")]
public class D3NodeViewModel
{
    [JsonProperty("id")]
    public string Id { get; set; }

/*
    [JsonProperty("sourceLinks")]
    public string SourceLinks { get; set; } //  - the array of outgoing links which have this node as their source
    
    [JsonProperty("targetLinks")]
    public string TargetLinks { get; set; } //  - the array of incoming links which have this node as their target
    
    [JsonProperty("value")]
    public int Value { get; set; } //  - the node’s value; the sum of link.value for the node’s incoming links
    
    [JsonProperty("index")]
    public int Index { get; set; } //  - the node’s zero-based index within the array of nodes
    
    [JsonProperty("depth")]
    public int Depth { get; set; } //  - the node’s zero-based graph depth, derived from the graph topology
    
    [JsonProperty("height")]
    public int Height { get; set; } //  - the node’s zero-based graph height, derived from the graph topology
    
    [JsonProperty("x0")]
    public int X0 { get; set; } //  - the node’s minimum horizontal position, derived from node.depth
    
    [JsonProperty("x1")]
    public int X1 { get; set; } //  - the node’s maximum horizontal position (node.x0 + sankey.nodeWidth)
    
    [JsonProperty("y0")]
    public int Y0 { get; set; } //  - the node’s minimum vertical position

    [JsonProperty("y1")]
    public int Y1 { get; set; } // - the node’s maximum vertical position (node.y1 - node.y0 is proportional to node.value)
*/
}