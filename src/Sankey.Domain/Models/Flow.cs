namespace Sankey.Domain.Models
{
    public class Flow : IEntity
    {
        public int Id { get; set; }

        public int GeoId { get; set; }

        public Geo Geo { get; set; }

        public int FuelId { get; set; }

        public Fuel Fuel { get; set; }

        public int SupplyId { get; set; }

        public Supply Supply { get; set; }

        public int Value { get; set; }

        public int Year { get; set; }
    }
}
