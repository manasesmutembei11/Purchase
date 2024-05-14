using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

       public virtual List<OrderItemDTO>? Items { get; set; }

        public virtual CustomerDTO? Customer { get; set; }

        public decimal Total { get; set; }
    }
}
