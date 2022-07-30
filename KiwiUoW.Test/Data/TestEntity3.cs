using KiwiUoW.Abstraction;

namespace KiwiUoW.Test.Data
{
    public class TestEntity3 : IEntity<int>
    {
        public int Id { get; set; }
        public int TestData { get; set; }
    }
}
