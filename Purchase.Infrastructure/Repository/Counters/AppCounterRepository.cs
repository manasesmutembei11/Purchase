using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.Contracts.Counters;
using Purchase.Domain.Models.Counters;

namespace Purchase.Infrastructure.Repository.Counters
{
    internal class AppCounterRepository : IAppCounterRepository
    {
        private readonly RepositoryContext _context;

        public AppCounterRepository(RepositoryContext context)
        {
            _context = context;
        }
        public int GetCounter(CounterType type)
        {
            var counter = _context.Counters.FirstOrDefault(s => s.CounterType == type);
            if (counter == null)
            {
                counter = new AppCounter
                {
                    Value = 0,
                    CounterType = type,
                    Id = Guid.NewGuid()
                };
                _context.Counters.Add(counter);
            }
            counter.Value++;
            _context.SaveChanges();
            return counter.Value;
        }
    }
}
