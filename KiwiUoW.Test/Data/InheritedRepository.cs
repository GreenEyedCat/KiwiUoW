using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiUoW.Test.Data
{
    public class InheritedRepository : GenericRepository<TestEntity2, Guid>
    {
        protected InheritedRepository(DbContext context) : base(context)
        {
        }

        public void Add6()
        {
            Add(new TestEntity2 { Id = Guid.NewGuid(), TestData = 6 });
        }
    }
}
