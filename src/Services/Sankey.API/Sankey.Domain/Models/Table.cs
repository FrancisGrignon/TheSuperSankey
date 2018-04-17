using System.Collections.Generic;

namespace Sankey.Domain.Models
{
    public class Table : IEntity, ICodeset
    {
        public int Id { get; set; }

        public string NameEn { get; set; }

        public string NameFr { get; set; }

        public string Tag { get; set; }

        public string NoteEn { get; set; }

        public string NoteFr { get; set; }

        public ICollection<Flow> Flows { get; set;}
    }
}
