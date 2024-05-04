﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public DateOnly OrderDate { get; set; }

        public decimal Total { get; set; }

        public virtual Customer? Customer { get; set; }

        public virtual ICollection<OrderItem>? OrderItems { get; set; }
    }
}
