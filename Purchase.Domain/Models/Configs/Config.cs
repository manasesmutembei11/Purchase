using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Configs
{
    [Table("app_configs")]
    public class Config
    {
        public Guid Id { get; set; }
        public ConfigType ConfigType { get; set; }
        public string? Value { get; set; }
        public string? Description { get; set; }
    }
}
