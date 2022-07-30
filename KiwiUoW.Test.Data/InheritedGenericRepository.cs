using KiwiUoW.Abstraction;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiUoW.Test.Data
{
    public class InheritedGenericRepository<T, TKey> : GenericRepository<T, TKey> where T : class, IEntity<TKey>
    {
        private readonly IMemoryCache _cache;

        protected InheritedGenericRepository(DbContext context) : base(context)
        {
            var options = new MemoryCacheOptions();
            _cache = new MemoryCache(options);
        }

        public override T Get(TKey id)
        {
            if(_cache.TryGetValue(CacheKey(id), out var fromCache))
            {
                return (T)fromCache;
            }
            else
            {
                var item = base.Get(id);
                _cache.Set(CacheKey(item.Id), item, TimeSpan.FromHours(1));
                return item;
            }
        }

        private string CacheKey(TKey id)
        {
            var type = typeof(T).FullName;
            return $"{type}__{id}";
        }
    }
}
