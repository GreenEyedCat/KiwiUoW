using KiwiUoW.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KiwiUoW
{
    public class GenericRepository<T, TKey> where T: class, IEntity<TKey>
    {
        private readonly DbContext _context;

        public IQueryable<T> All => _context.Set<T>();

        protected GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual T Get(TKey id)
        {
            return _context.Find<T>(id);
        }

        public virtual void Add(T item)
        {
            _context.Add(item);
        }

        public virtual void Update(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public virtual void Remove(T item)
        {
            _context.Remove(item);
        }

        public virtual async Task<T> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await _context.FindAsync<T>(new object[] { id }, cancellationToken);
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            _context.AddRange(items);
        }

        public virtual void UpdateRange(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                _context.Entry(item).State = EntityState.Modified;
            }
        }

        public virtual void RemoveRange(IEnumerable<T> items)
        {
            _context.RemoveRange(items);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
