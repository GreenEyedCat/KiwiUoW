using KiwiUoW.Test.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiUoW.Test
{
    public class RepositoryTest
    {
        private readonly TestDbContext _context;

        public RepositoryTest()
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
            _context.SaveChanges();
        }

        [Test]
        public void InsertData()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();
            var id = Guid.NewGuid();

            repo.Add(new TestEntity()
            {
                Id = id,
                TestData = 5
            });
            repo.SaveChanges();

            Assert.AreEqual(1, _context.TestEntities.Count());
            Assert.AreEqual(5, _context.TestEntities.Find(id)?.TestData);
        }

        [Test]
        public void GetData()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();
            var id = Guid.NewGuid();

            _context.TestEntities.Add(new TestEntity()
            {
                Id = id,
                TestData = 5
            });
            _context.SaveChanges();

            Assert.AreEqual(5, repo.Get(id).TestData);
        }

        [Test]
        public void RemoveData()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();
            var id = Guid.NewGuid();

            var testEntity = new TestEntity()
            {
                Id = id,
                TestData = 5
            };
            _context.TestEntities.Add(testEntity);
            _context.SaveChanges();

            repo.Remove(testEntity);
            repo.SaveChanges();

            Assert.AreEqual(null, repo.Get(id));
        }

        [Test]
        public async Task GetDataAsync()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();
            var id = Guid.NewGuid();

            _context.TestEntities.Add(new TestEntity()
            {
                Id = id,
                TestData = 5
            });
            _context.SaveChanges();

            Assert.AreEqual(5, (await repo.GetAsync(id)).TestData);
        }

        [Test]
        public async Task UpdateData()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();
            var id = Guid.NewGuid();

            var testEntity = new TestEntity()
            {
                Id = id,
                TestData = 5
            };
            _context.TestEntities.Add(testEntity);
            await _context.SaveChangesAsync();

            testEntity.TestData = 7;
            repo.Update(testEntity);
            await repo.SaveChangesAsync();

            Assert.AreEqual(7, repo.Get(id).TestData);
        }

        [Test]
        public void InsertMultipleData()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();
            
            var data = new List<TestEntity>();
            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                data.Add(new TestEntity()
                {
                    Id = id,
                    TestData = 5
                });
            }

            repo.AddRange(data);
            repo.SaveChanges();

            Assert.AreEqual(5, _context.TestEntities.Count());
        }

        [Test]
        public void UpdateMultipleData()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();

            var data = new List<TestEntity>();
            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                data.Add(new TestEntity()
                {
                    Id = id,
                    TestData = 5
                });
            }

            _context.AddRange(data);
            _context.SaveChanges();

            foreach (var entity in data)
            {
                entity.TestData = 7;
            }

            repo.UpdateRange(data);
            repo.SaveChanges();

            Assert.AreEqual(35, _context.TestEntities.Sum(x => x.TestData));
        }

        [Test]
        public void RemoveMultipleData()
        {
            var repoFactory = new RepositoryFactory(_context);
            var repo = repoFactory.Build<TestEntity, Guid>();

            var data = new List<TestEntity>();
            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                data.Add(new TestEntity()
                {
                    Id = id,
                    TestData = 5
                });
            }

            _context.AddRange(data);
            _context.SaveChanges();

            repo.RemoveRange(repo.All);
            repo.SaveChanges();

            Assert.AreEqual(0, _context.TestEntities.Count());
        }
    }
}
