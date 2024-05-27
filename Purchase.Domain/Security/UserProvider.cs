using Purchase.Domain.Contracts;
using Purchase.Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Security
{
    public class AppUserProvider : IUserProvider
    {
        public UserRef User => GetUser();

        private UserRef GetUser()
        {
            var user = new UserRef() { Username = "unauthenticated", Id = Guid.Empty };
            if (MyHttpContext.Current != null)
            {
                var online = new AppUser(MyHttpContext.Current.User);


                if (online != null && !string.IsNullOrEmpty(online.Username))
                {
                    user.Authenticated = true;
                    user.Id = online.UserId;
                    user.Username = online.Username;
                    user.FullName = online.FullName;
                    user.Accounts = online.Accounts;

                }
            }
            return user;

        }

        public string ApplicationName => "OAS";

        public string IpAddress => GetIP();

        private static string GetIP()
        {
            return MyHttpContext.Current != null ? MyHttpContext.Current.Connection.RemoteIpAddress.ToString() : "127.0.0.1";

        }

    }
}
