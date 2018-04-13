namespace Sankey.Domain.Models
{
    public class Node : IEntity
    {
        public int Id { get; set; }

        public string NameFr { get; set; }

        public string NameEn { get; set; }
    }
}
