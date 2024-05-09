using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public class OrderItem : BaseEntity<Guid>
    {

        public Guid ProductId { get; set; }

        public Guid OrderId { get; set; }

        public virtual Product? Product { get; set; }

        public virtual Order? Order { get; set; }

        public int Quantity { get; set; }

        public decimal SubTotal { get; set; }


        public virtual Tax? Tax {  get; set; }


    }
}
