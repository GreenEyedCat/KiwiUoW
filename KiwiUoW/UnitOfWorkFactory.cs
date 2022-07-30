using KiwiUoW.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KiwiUoW
{
    public class UnitOfWorkFactory<T> where T : UnitOfWork
    {
        private DbContext _context;
        private RepositoryFactory _repositoryFactory;

        public UnitOfWorkFactory(DbContext context, RepositoryFactory repositoryFactory)
        {
            _context = context;
            _repositoryFactory = repositoryFactory;
        }

        public T Build()
        {
            var instance = Build(typeof(T).GetConstructors(), _context);
            AddRepositories(instance);
            return instance;
        }

        private void AddRepositories(T instance)
        {
            foreach(var prop in typeof(T).GetProperties())
            {
                if (prop.PropertyType.IsSubclassOfGeneric(typeof(GenericRepository<,>)))
                {
                    var repo = _repositoryFactory.Build(prop.PropertyType);
                    prop.SetValue(instance, repo);
                }
            }
        }

        private T Build(ConstructorInfo[] constructors, DbContext context)
        {
            foreach(var constructor in constructors)
            {
                if(constructor.GetParameters().Length == 1 &&
                    constructor.GetParameters()[0].ParameterType.IsSameOrSubclass(typeof(DbContext)))
                {
                    return (T)constructor.Invoke(new[] { context });
                }
            }
            return null;
        }
    }
}
