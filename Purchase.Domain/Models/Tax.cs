using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public class Tax : BaseEntity<Guid>
    {

        public string? Code { get; set; }

        public string? Name { get; set;}

        public decimal Rate { get; set; }
    }
}
