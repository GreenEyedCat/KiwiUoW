using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiwiUoW.Test.Data
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        public DbSet<TestEntity> TestEntities { get; set; }
        public DbSet<TestEntity2> TestEntities2 { get; set; }
        public DbSet<TestEntity3> TestEntities3 { get; set; }
    }
}
