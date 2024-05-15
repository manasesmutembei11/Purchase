using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public class Product : BaseEntity<Guid>
    { 

        public string? Code { get; set; }
        public string? Name { get; set; }

        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public string? Description { get; set; }

        public Guid CategoryId { get; set; }
        public virtual Category? Category { get; set; }
        public Guid TaxId { get; set; }
        public virtual Tax? Tax { get; set; }

    }
}
