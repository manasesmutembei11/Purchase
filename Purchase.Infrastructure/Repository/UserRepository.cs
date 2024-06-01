using Purchase.Domain.Contracts;
using Purchase.Domain.Models.UserEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly RepositoryContext _context;

        public UserRepository(RepositoryContext context)
        {
            _context = context;
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            return _context.Set<User>().AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
        }
    }
}
