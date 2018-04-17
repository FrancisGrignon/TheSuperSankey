namespace Sankey.Domain.Models
{
    public class Geo : IEntity, ICodeset
    {
        public int Id { get; set; }

        public string NameFr { get; set; }

        public string NameEn { get; set; }

        public string Iso3166 { get; set; }
    }
}
