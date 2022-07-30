using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace KiwiUoW.AspNetCore
{
    public static class BuilderExtensions
    {
        public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : UnitOfWork
        {
            services.AddScoped<RepositoryFactory>();

            services.AddScoped<UnitOfWorkFactory<T>>();
            services.AddScoped<T>(x => x.GetRequiredService<UnitOfWorkFactory<T>>().Build());

            return services;
        }
    }
}
