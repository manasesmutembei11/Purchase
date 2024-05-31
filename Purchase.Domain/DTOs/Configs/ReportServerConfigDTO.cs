using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs.Configs
{
    public class ConfigBaseDTO
    {
        public int ConfigType { get; set; }
    }
    public class ReportServerConfigDTO : ConfigBaseDTO
    {
        [Required]
        public string ReportServerUri { get; set; }
        [Required]
        public string ReportServerUsername { get; set; }
        [Required]
        public string ReportServerPassword { get; set; }
        public string Scheme { get; set; }

    }

    public class ExpaqMateServerConfigDTO : ConfigBaseDTO
    {
        [Required]
        public string ServerUri { get; set; }


    }
}
