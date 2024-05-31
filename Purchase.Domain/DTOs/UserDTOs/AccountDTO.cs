using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs.UserDTOs
{
    public class AccountRefDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? TypeName { get; set; }

    }
    public abstract class AccountDTO
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? TaxNo { get; set; }
        public string? PostalAddress { get; set; }
        public string? PostalCode { get; set; }
        public string? PhysicalAddress { get; set; }
        public string? ContactPersonName { get; set; }
        public string? ContactPersonPhoneNumber { get; set; }
        public string? ContactPersonEmail { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;
    }
}
