﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public enum AccountType
    {
        [Description("Assessor")]
        Assessor = 1,
        [Description("Client")]
        Client = 2,

    }
    public abstract class Account : BaseEntity<Guid>
    {
        public abstract AccountType AccountType { get; protected set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string TaxNo { get; set; }
        public string PostalAddress { get; set; }
        public string PostalCode { get; set; }
        public string PhysicalAddress { get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonPhoneNumber { get; set; }
        public string ContactPersonEmail { get; set; }
        public string Street { get; set; } = string.Empty;
        public string Town { get; set; } = string.Empty;


    }
}
