using Microsoft.Extensions.Options;
using NetCoreRedis.Models.Settings;
using StackExchange.Redis;

namespace NetCoreRedis.Services.Redises.ExchangeAPI
{
    /// <summary>
    /// Redis StackExchange API kullanımı
    /// Nuget Package Manager: StackExchange.Redis
    /// </summary>
    public class RedisStackExchangeAPI : IRedisStackExchangeAPI
    {
        private readonly RedisSettings _redisSettings;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisStackExchangeAPI(IOptions<RedisSettings> redisSettings)
        {
            _redisSettings = redisSettings.Value;
        }

        /// <summary>
        /// Middleware(Configure) tarafında ConnectAsync metotunu çağırıp, redis ile bağlantısını kuruyorum.
        /// </summary>
        public async void ConnectServer()
        {
            string serverAddress = string.Concat(_redisSettings.Host, ":", _redisSettings.Port);
            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(serverAddress);
        }

        /// <summary>
        /// Redis tarafında işlem yapmak istediğim veri tabanını belirtiyorum
        /// </summary>
        /// <param name="databaseIndex">veri tabanı index</param>
        /// <returns>IDatabase</returns>
        public IDatabase GetSelectedDatabase(int databaseIndex = 0)
        {
            return _connectionMultiplexer.GetDatabase(databaseIndex);
        }
    }
}
