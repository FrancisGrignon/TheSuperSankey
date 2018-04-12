namespace Sankey.Domain.Models
{
    public class Geo : IEntity
    {
        public int Id { get; set; }

        public string NameFr { get; set; }

        public string NameEn { get; set; }

        public string Alpha2 { get; set; }
    }
}
