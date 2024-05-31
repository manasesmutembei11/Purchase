using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Configs
{
    public class ReportServerConfig : ConfigBase
    {
        [Required]
        public string ReportServerUri { get; set; }
        [Required]
        public string ReportServerUsername { get; set; }
        [Required]
        public string ReportServerPassword { get; set; }
        public string Scheme { get; set; }
        public override ConfigType ConfigType { get => ConfigType.ReportServer; }
    }
}
