using KiwiUoW.Abstraction;
using System;

namespace KiwiUoW.Test.Data
{
    public class TestEntity2 : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public int TestData { get; set; }
    }
}
