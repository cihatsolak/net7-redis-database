using Microsoft.Extensions.Options;
using NetCoreRedis.Models.Settings;
using StackExchange.Redis;
using System;

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
            var configurationOptions = new ConfigurationOptions()
            {
                EndPoints = { string.Concat(_redisSettings.Host, ":", _redisSettings.Port) },
                AbortOnConnectFail = _redisSettings.AbortOnConnectFail,
                AsyncTimeout = _redisSettings.AsyncTimeOutMilliSecond,
                ConnectTimeout = _redisSettings.ConnectTimeOutMilliSecond
            };

            _connectionMultiplexer = await ConnectionMultiplexer.ConnectAsync(configurationOptions);
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
