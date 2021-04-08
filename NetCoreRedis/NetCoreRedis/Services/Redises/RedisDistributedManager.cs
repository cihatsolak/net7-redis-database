using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace NetCoreRedis.Services.Redises
{
    /// <summary>
    /// Redis Server: IDistributedCache 
    /// </summary>
    public partial class RedisDistributedManager : IRedisDistributedService
    {
        //using Microsoft.Extensions.Caching.Distributed;
        private readonly IDistributedCache _distributedCache;
        public RedisDistributedManager(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// Cachelediğimiz veriyi çağırmak
        /// </summary>
        /// <typeparam name="TEntity">Veri tipi</typeparam>
        /// <param name="key">Cachelenilen ad</param>
        /// <returns></returns>
        public TEntity GetObject<TEntity>(string key)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Cachelediğimiz veriyi çağırmak (Asenkron)
        /// </summary>
        /// <typeparam name="TEntity">Veri tipi</typeparam>
        /// <param name="key">Cachelenilen ad</param>
        /// <returns></returns>
        public Task<TEntity> GetObjectAsync<TEntity>(string key)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// İstediğimiz tür de veriyi cachelemek
        /// </summary>
        /// <typeparam name="TEntity">Cachelenecek tip</typeparam>
        /// <param name="key">Veri tipi</param>
        /// <param name="value">Değer</param>
        /// <param name="absoluteExpirationMinute">Süre</param>
        /// <param name="slidingExpirationSecond">Uzama Süresi</param>
        public void SetObject<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 1, int slidingExpirationSecond = 10)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// İstediğimiz tür de veriyi cachelemek (Asenkron)
        /// </summary>
        /// <typeparam name="TEntity">Cachelenecek tip</typeparam>
        /// <param name="key">Veri tipi</param>
        /// <param name="value">Değer</param>
        /// <param name="absoluteExpirationMinute">Süre</param>
        /// <param name="slidingExpirationSecond">Uzama Süresi</param>
        public Task SetObjectAsync<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 1, int slidingExpirationSecond = 10)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Cache silme
        /// </summary>
        /// <param name="key">Cachelenen ad</param>
        public void Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Silinecek cache adı (Asenkron)
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        public Task RemoveAsync(string key)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Dosya vb. redis cache
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        /// <param name="value">byte değer</param>
        public void FileCache(string key, byte[] value)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Dosya vb. redis cache (Asenkron)
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        /// <param name="value">byte değer</param>
        public Task FileCacheAsync(string key, byte[] value)
        {
            throw new System.NotImplementedException();
        }
    }
}
