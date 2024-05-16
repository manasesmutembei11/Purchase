using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public class Category : BaseEntity<Guid>
    { 
        public string? Name { get; set; }
        public string? Code { get; set; }

        public string? Description { get; set; }
        public virtual ICollection<Product>? Products { get; set; }

    }
}
