using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using slignaraspnetcoreserver.EF;
using slignaraspnetcoreserver.Hubs;
using slignaraspnetcoreserver.Repository;
using slignaraspnetcoreserver.SqlTableDependencies;

namespace slignaraspnetcoreserver
{
    public class Startup
    {
        private const string ConnectionString = "data source=.; initial catalog=SignalRDemo; user id=sa; password=reallyStrongPwd123";
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            // services.AddSignalR();

            services.AddTransient<GaugeDatabaseSubscription>();
            // dependency injection
            services.AddDbContextFactory<GaugeContext>(ConnectionString);
            services.AddSingleton<IGaugeRepository, GaugeRepository>();
            // services.AddSingleton<IDatabaseSubscription, GaugeDatabaseSubscription>();
            // services.AddSingleton<IHubContext<GaugeHub>, HubContex<GaugeHub>();

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:4200");
            }));
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<GaugeHub>("/gauges");
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSqlTableDependency(ConnectionString);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }
    }
}
