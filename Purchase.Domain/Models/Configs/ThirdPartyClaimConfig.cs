using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.Configs
{
    public class ThirdPartyClaimConfig : ConfigBase
    {

        public override ConfigType ConfigType { get => ConfigType.ThirdPartyClaims; }

        [Required]
        public Guid TracingClaimNatureID { get; set; }
    }
}
