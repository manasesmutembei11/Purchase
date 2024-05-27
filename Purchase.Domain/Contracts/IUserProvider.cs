using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public class UserRef
    {
        public Guid? Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public bool Authenticated { get; set; }
        public Guid? CountyId { get; set; }

        public Guid[] Accounts { get; set; } = new List<Guid>().ToArray();
    }
    public interface IUserProvider
    {
        UserRef User { get; }
        string ApplicationName { get; }
        string IpAddress { get; }
    }
}
