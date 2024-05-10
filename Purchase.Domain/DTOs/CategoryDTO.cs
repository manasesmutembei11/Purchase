﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public string? Description { get; set; }
    }
}
