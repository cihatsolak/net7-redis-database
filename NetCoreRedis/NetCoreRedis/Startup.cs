using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCoreRedis.Models.Settings;
using NetCoreRedis.Services;
using NetCoreRedis.Services.Memories;
using NetCoreRedis.Services.Redises.Distributed;
using NetCoreRedis.Services.Redises.ExchangeAPI;

namespace NetCoreRedis
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            #region InMemoryCache Konfigürasyonu
            services.AddMemoryCache();
            services.AddSingleton<IMemoryService, MemoryManager>();
            #endregion

            #region DistributedCache Konfigürasyonu
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost:6379";
            });
            services.AddSingleton<IRedisDistributedService, RedisDistributedManager>();
            #endregion

            #region StackExchange.Redis Konfigürasyonu
            services.AddSingleton<RedisConfiguration>();
            services.Configure<RedisSettings>(Configuration.GetSection(nameof(RedisSettings)));
            #endregion
        }

        public void Configure(IApplicationBuilder app, RedisConfiguration redisConfiguration)
        {
            redisConfiguration.ConnectServer();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
