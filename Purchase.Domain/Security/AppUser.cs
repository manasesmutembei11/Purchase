using Purchase.Domain.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Security
{
    public class AppUser : ClaimsPrincipal
    {
        public AppUser(ClaimsPrincipal principal)
            : base(principal)
        {
        }
        public Guid UserId
        {
            get
            {
                Claim claim = this.FindFirst("userId");
                Guid.TryParse(claim?.Value, out Guid userId);
                return userId;
            }
        }
        public Guid[] Accounts
        {
            get
            {
                List<Guid> ids = new List<Guid>();
                List<Claim> claims = this.Claims.Where(s => s.Type == CustomClaimTypes.Account).ToList();
                foreach (Claim claim in claims)
                {
                    if (Guid.TryParse(claim?.Value, out Guid acountId))
                    {
                        ids.Add(acountId);
                    }
                }
                return ids.ToArray();
            }
        }





        public string Username
        {
            get
            {
                Claim claim = this.FindFirst("username");
                return claim?.Value;
            }
        }
        public string FullName
        {
            get
            {
                Claim claimFirstName = this.FindFirst("firstName");
                Claim claimLastName = this.FindFirst("lastName");
                return $"{claimFirstName?.Value} {claimLastName?.Value}".Trim();

            }
        }






    }
}
