using KiwiUoW.Abstraction;
using KiwiUoW.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace KiwiUoW
{
    public class RepositoryFactory
    {
        private readonly DbContext _context;
        private readonly ConcurrentDictionary<string, object> _repositories;

        public RepositoryFactory(DbContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, object>();
        }

        public GenericRepository<T, TKey> Build<T, TKey>() where T : class, IEntity<TKey>
        {
            return (GenericRepository<T, TKey>)Build(typeof(GenericRepository<T, TKey>));
        }

        public object Build(Type type)
        {
            if(_repositories.TryGetValue(type.FullName, out object value))
            {
                return value;
            }
            value = BuildRepo(type.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance), _context);
            if (_repositories.TryAdd(type.FullName, value))
            {
                return value;
            }
            else
            {
                return Build(type);
            }    
        }

        private object BuildRepo(ConstructorInfo[] constructors, DbContext context)
        {
            foreach (var constructor in constructors)
            {
                if (constructor.GetParameters().Length == 1 &&
                    constructor.GetParameters()[0].ParameterType.IsSameOrSubclass(typeof(DbContext)))
                {
                    return constructor.Invoke(new[] { context });
                }
            }
            return null;
        }
    }
}
