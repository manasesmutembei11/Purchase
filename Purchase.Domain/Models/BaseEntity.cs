using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models
{
    public enum EntityStatus
    {
        Inactive = 0,
        Active = 1,
        Deleted = 2


    }

    public class BaseEntity<T> 
    {
        [Required]
        public T Id { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        [Required]
        public EntityStatus Status { get; set; }
    }
}
