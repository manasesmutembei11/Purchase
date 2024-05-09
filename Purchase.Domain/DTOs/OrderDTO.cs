﻿using Purchase.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs
{
    public class OrderDTO
    {
        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public string CustomerName { get; set; } 

        public decimal Total { get; set; }
    }
}
