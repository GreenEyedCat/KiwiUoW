using KiwiUoW.Test.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace KiwiUoW.AspNetCore.Test
{
    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder().Configure(builder => {}).ConfigureServices(services =>
            {
                services.AddDbContext<DbContext, TestDbContext>(options =>
                {
                    SqliteDbContextOptionsBuilderExtensions.UseSqlite(options, "Filename=:memory:");
                });
                services.AddUnitOfWork<TestUoW>();
            });
        }

        public static void Main(string[] args)
        {
        }
    }
}