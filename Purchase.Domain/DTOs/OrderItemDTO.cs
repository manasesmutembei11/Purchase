using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs
{
    public class OrderItemDTO
    {
        public Guid Id { get; set; }

        public virtual List<Product>? Products { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxRate { get; set; } 

        public Guid OrderId { get; set; }
    }
}
