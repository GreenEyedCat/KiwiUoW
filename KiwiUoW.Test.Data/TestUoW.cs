using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiUoW.Test.Data
{
    public class TestUoW : UnitOfWork
    {
        public TestUoW(DbContext context) : base(context)
        {
        }

        public GenericRepository<TestEntity, Guid> TestEntity { get; set; }
        public InheritedRepository TestEntity2 { get; set; }
        public InheritedGenericRepository<TestEntity3, int> TestEntity3 { get; set; }
    }
}
