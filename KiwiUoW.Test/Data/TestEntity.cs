using KiwiUoW.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiUoW.Test.Data
{
    public class TestEntity : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int TestData { get; set; }
    }
}
