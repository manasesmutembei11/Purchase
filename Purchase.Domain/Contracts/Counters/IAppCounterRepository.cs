using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.Models.Counters;

namespace Purchase.Domain.Contracts.Counters
{
    public interface IAppCounterRepository
    {
        int GetCounter(CounterType type);
    }
}
