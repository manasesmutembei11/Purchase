using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Purchase.Domain.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Purchase.Infrastructure.Caching
{
    internal class DefaultCacheProvider : ICacheProvider
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<DefaultCacheProvider> _logger;

        public DefaultCacheProvider(IMemoryCache memoryCache, ILogger<DefaultCacheProvider> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
            _logger.LogDebug("create DefaultCacheProvider");
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public void Put(string key, object value)
        {
            var cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(30));
            _memoryCache.Set(key, value, cacheOptions);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }
    }
}
