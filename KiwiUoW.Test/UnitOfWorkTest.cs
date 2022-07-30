using KiwiUoW.Test.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KiwiUoW.Test
{
    public class UnitOfWorkTest
    {
        private readonly TestDbContext _context;

        public UnitOfWorkTest()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
                .UseSqlite("Filename=:memory:")
                .Options;

            _context = new TestDbContext(contextOptions);
        }

        [SetUp]
        public void Setup()
        {
            _context.TestEntities.RemoveRange(_context.TestEntities);
            _context.TestEntities2.RemoveRange(_context.TestEntities2);
            _context.TestEntities3.RemoveRange(_context.TestEntities3);
            _context.SaveChanges();
        }

        [Test]
        public void CommitTransation()
        {
            var repoFactory = new RepositoryFactory(_context);
            var factory = new UnitOfWorkFactory<TestUoW>(_context, repoFactory);
            var uow = factory.Build();

            uow.BeginTransaction();
            uow.TestEntity.Add(new TestEntity()
            {
                Id = Guid.NewGuid(),
                TestData = 5
            });
            uow.CommitTransaction();
            uow.SaveChanges();

            Assert.AreEqual(1, _context.TestEntities.Count());
            Assert.AreEqual(5, _context.TestEntities.First().TestData);
        }

        [Test]
        public async Task RollbackTransation()
        {
            var repoFactory = new RepositoryFactory(_context);
            var factory = new UnitOfWorkFactory<TestUoW>(_context, repoFactory);
            var uow = factory.Build();

            uow.BeginTransaction();
            uow.TestEntity.Add(new TestEntity()
            {
                Id = Guid.NewGuid(),
                TestData = 5
            });
            await uow.SaveChangesAsync();
            uow.RollbackTransaction();
            await uow.SaveChangesAsync();

            Assert.AreEqual(0, _context.TestEntities.Count());
        }

        [Test]
        public void InheritedRepository()
        {
            var repoFactory = new RepositoryFactory(_context);
            var factory = new UnitOfWorkFactory<TestUoW>(_context, repoFactory);
            var uow = factory.Build();

            uow.TestEntity2.Add6();
            uow.TestEntity2.SaveChanges();

            Assert.AreEqual(1, _context.TestEntities2.Count());
            Assert.AreEqual(6, _context.TestEntities2.First().TestData);
        }

        [Test]
        public void InheritedGenericRepository()
        {
            var repoFactory = new RepositoryFactory(_context);
            var factory = new UnitOfWorkFactory<TestUoW>(_context, repoFactory);
            var uow = factory.Build();

            uow.TestEntity3.Add(new TestEntity3()
            {
                TestData = 7
            });
            uow.TestEntity3.SaveChanges();

            Assert.AreEqual(1, _context.TestEntities3.Count());
            Assert.AreEqual(7, _context.TestEntities3.First().TestData);
        }
    }
}