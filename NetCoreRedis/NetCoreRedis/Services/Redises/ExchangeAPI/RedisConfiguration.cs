using Microsoft.Extensions.Options;
using NetCoreRedis.Models.Settings;
using StackExchange.Redis;

namespace NetCoreRedis.Services.Redises.ExchangeAPI
{
    /// <summary>
    /// Middleware(Configure) tarafında ConnectAsync metotunu çağırıyorum.
    /// </summary>
    public class RedisConfiguration
    {
        private readonly RedisSettings _redisSettings;
        private ConnectionMultiplexer _connectionMultiplexer;

        public RedisConfiguration(IOptions<RedisSettings> redisSettings)
        {
            _redisSettings = redisSettings.Value;
        }

        public async void ConnectServer()
        {
            string serverAddress = string.Concat(_redisSettings.Host, ":", _redisSettings.Port);
            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(serverAddress);
        }

        public IDatabase GetSelectedDatabase(int databaseIndex = 0)
        {
            return _connectionMultiplexer.GetDatabase(databaseIndex);
        }
    }
}
