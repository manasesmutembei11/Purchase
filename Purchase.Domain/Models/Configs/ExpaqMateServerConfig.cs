using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Configs
{
    public class ExpaqMateServerConfig : ConfigBase
    {
        [Required]
        public string? ServerUri { get; set; }

        public override ConfigType ConfigType { get => ConfigType.ExpaqMateServer; }
    }
}
