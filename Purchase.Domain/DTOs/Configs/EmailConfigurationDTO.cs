using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs.Configs
{
    public class EmailConfigurationDTO : ConfigBaseDTO
    {
        public string From { get; set; }
        public string Name { get; set; }
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public string SmtpUserName { get; set; }
        public string SmtpPassword { get; set; }


    }
}
