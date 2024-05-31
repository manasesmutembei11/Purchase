using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Configs
{
    public abstract class ConfigBase
    {
        public abstract ConfigType ConfigType { get; }
    }
}
