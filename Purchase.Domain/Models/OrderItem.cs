using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public class OrderItem : BaseEntity<Guid>
    {

        public Guid OrderId { get; set; }
        public virtual Order? Order { get; set; }
        public Guid ProductId { get; set; }  
        public virtual Product? Product { get; set; }
        public string? label { get; set; }
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

    }
}
