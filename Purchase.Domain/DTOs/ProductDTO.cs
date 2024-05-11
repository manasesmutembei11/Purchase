using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }

        public string? Code { get; set; }
        public string? Name { get; set; }

        public decimal Price { get; set; }

        public bool HasTax { get; set; }

        public decimal TaxRate { get; set; }

        public Guid CategoryId { get; set; }
     

        public int Quantity { get; set; }
        public string? Description { get; set; }
    }
}
