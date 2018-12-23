using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace slignaraspnetcoreserver
{
    public static class AddDbContextFactoryHelper
    {
        public static void AddDbContextFactory<T>(this IServiceCollection services, string connectionString) where T : DbContext
        {
            services.AddSingleton<Func<T>>((ctx) =>
            {
                var options = new DbContextOptionsBuilder()
                    .UseSqlServer(connectionString)
                    .Options;

                return () => (T)Activator.CreateInstance(typeof(T), options);
            });
        }
    }
}