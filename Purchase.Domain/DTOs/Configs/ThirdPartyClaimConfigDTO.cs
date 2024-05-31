using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.DTOs.Configs
{
    public class ThirdPartyClaimConfigDTO : ConfigBaseDTO
    {
        public Guid? TracingClaimNatureID { get; set; }
    }
}
