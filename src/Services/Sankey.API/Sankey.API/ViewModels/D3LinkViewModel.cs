using Newtonsoft.Json;

[JsonObject("Links")]
public class D3LinkViewModel
{
    // Each link must be an object with the following properties:
    [JsonProperty("source")]
    public string Source { get; set; } //- the link’s source node
    
    [JsonProperty("target")]
    public string Target { get; set; } //- the link’s target node
    
    [JsonProperty("value")]
    public int Value { get; set; } //- the link’s numeric value

/* 
    // For convenience, a link’s source and target may be initialized using numeric or string identifiers rather than object references; ; see sankey.nodeId. The following properties are assigned to each link by the Sankey generator:
    [JsonProperty("y0")]
    public string Y0 { get; set; } //- the link’s vertical starting position (at source node)
    
    [JsonProperty("y1")]
    public string Y1 { get; set; } //- the link’s vertical end position (at target node)
    
    [JsonProperty("width")]
    public string Width { get; set; } //- the link’s width (proportional to link.value)
    
    [JsonProperty("index")]
    public string Index { get; set; } //- the zero-based index of link within the array of links
*/
}