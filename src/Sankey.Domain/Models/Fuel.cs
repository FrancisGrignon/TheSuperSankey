using System;
using System.Collections.Generic;
using System.Text;

namespace Sankey.Domain.Models
{
    public class Fuel : IEntity
    {
        public int Id { get; set; }

        public string NameFr { get; set; }

        public string NameEn { get; set; }
    }
}
