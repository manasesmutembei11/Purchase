using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public class Order : BaseEntity<Guid>
    {

        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public  List<string>? ItemNames { get; set; }

        public decimal Total { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
