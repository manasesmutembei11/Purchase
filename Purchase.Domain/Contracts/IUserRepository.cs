using Purchase.Domain.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Contracts
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
    }
}
