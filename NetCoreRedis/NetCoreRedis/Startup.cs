using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetCoreRedis.Services;
using NetCoreRedis.Services.Memories;
using NetCoreRedis.Services.Redises;

namespace NetCoreRedis
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddMemoryCache(); //InMemoryCache servisini kullanmak için

            // IDistributedCache ayarlamasý
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });

            services.AddSingleton<IMemoryService, MemoryManager>();
            services.AddSingleton<IRedisDistributedService, RedisDistributedManager>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
