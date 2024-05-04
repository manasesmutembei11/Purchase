using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Presentation.DTOs
{
    public class OrderItemDTO
    {
        public Guid OrderItemId { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; } 

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }

        public decimal TaxRate { get; set; } 

        public Guid OrderId { get; set; }
    }
}
