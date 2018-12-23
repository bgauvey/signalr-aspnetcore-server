using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using slignaraspnetcoreserver.SqlTableDependencies;

namespace slignaraspnetcoreserver
{
    public static class UseSqlTableDependencyHelpers
    {
        public static void UseSqlTableDependency(this IApplicationBuilder services, string connectionString)
        {
            var serviceProvider = services.ApplicationServices;
            var subscription = (GaugeDatabaseSubscription)serviceProvider.GetService(typeof(GaugeDatabaseSubscription));
            subscription.Configure(connectionString);
        }
    }
}