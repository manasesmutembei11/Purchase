using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Configs
{
    public enum ConfigType
    {
        [Description("Report Server")]
        ReportServer = 1,
        [Description("Storage")]
        Storage = 2,
        [Description("Email Server")]
        EmailServer = 3,

        [Description("Third Party Claims")]
        ThirdPartyClaims = 4,
        [Description("ExpaqMate Server")]
        ExpaqMateServer = 5,
    }
}
