using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Models.UserEntities
{
    public class RoleClaimPermission
    {
        private readonly string _permission;
        private readonly string _description;
        private readonly string _module;
        private readonly string _claimType;

        public RoleClaimPermission(string permission, string description, string module)
        {
            _permission = permission;
            _description = description;
            _module = module;
            _claimType = CustomClaimTypes.Permission;
        }

        public string Module => _module;

        public string Description => _description;

        public string Permission => _permission;

        public string ClaimType => _claimType;
    }
}
