using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Domain.Caching
{
    public interface ICacheProvider
    {
        void Put(string key, object value);
        object Get(string key);
        void Remove(string key);
    }
}
