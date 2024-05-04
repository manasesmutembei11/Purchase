using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Presentation.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; } 

        public DateOnly OrderDate { get; set; }

        public decimal Total { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
