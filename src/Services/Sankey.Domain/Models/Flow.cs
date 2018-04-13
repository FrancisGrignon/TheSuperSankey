namespace Sankey.Domain.Models
{
    public class Flow : IEntity
    {
        public int Id { get; set; }

        public Geo Geo { get; set; }

        public int GeoId { get; set; }

        public Node Source { get; set; }

        public int SourceId { get; set; }

        public Node Target { get; set; }

        public int TargetId { get; set; }

        public int Value { get; set; }

        public int Year { get; set; }

        public string Tag { get; set; }
    }
}
