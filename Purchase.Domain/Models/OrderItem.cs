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

        public virtual List<Product>? Products { get; set; } = new List<Product>();
        public virtual Order? Order { get; set; }


        public decimal SubTotal { get; set; }


        public virtual Tax? Tax {  get; set; }


    }
}
