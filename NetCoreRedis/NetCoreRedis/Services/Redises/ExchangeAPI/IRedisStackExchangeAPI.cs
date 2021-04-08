using StackExchange.Redis;

namespace NetCoreRedis.Services.Redises.ExchangeAPI
{
    /// <summary>
    /// Redis StackExchange API kullanımı
    /// Nuget Package Manager: StackExchange.Redis
    /// </summary>
    public interface IRedisStackExchangeAPI
    {
        /// <summary>
        /// Middleware(Configure) tarafında ConnectAsync metotunu çağırıp, redis ile bağlantısını kuruyorum.
        /// </summary>
        void ConnectServer();

        /// <summary>
        /// Redis tarafında işlem yapmak istediğim veri tabanını belirtiyorum
        /// </summary>
        /// <param name="databaseIndex">veri tabanı index</param>
        /// <returns>IDatabase</returns>
        IDatabase GetSelectedDatabase(int databaseIndex = 0);
    }
}
