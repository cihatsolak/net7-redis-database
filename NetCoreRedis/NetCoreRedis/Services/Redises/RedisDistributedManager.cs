using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetCoreRedis.Services.Redises
{
    /// <summary>
    /// Redis Server: IDistributedCache 
    /// Nuget Package Manager: Microsoft.Extensions.Caching.StackExchangeRedis
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
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            string cachedValue = _distributedCache.GetString(key);
            if (string.IsNullOrEmpty(cachedValue))
                return default;

            return JsonSerializer.Deserialize<TEntity>(cachedValue);
        }

        /// <summary>
        /// Cachelediğimiz veriyi çağırmak (Asenkron)
        /// </summary>
        /// <typeparam name="TEntity">Veri tipi</typeparam>
        /// <param name="key">Cachelenilen ad</param>
        /// <returns></returns>
        public async Task<TEntity> GetObjectAsync<TEntity>(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            string cachedValue = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cachedValue))
                return default;

            return JsonSerializer.Deserialize<TEntity>(cachedValue);
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
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinute),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSecond)
            };

            var serilazedValue = JsonSerializer.Serialize(value);

            _distributedCache.SetString(key, serilazedValue, distributedCacheEntryOptions);
        }

        /// <summary>
        /// İstediğimiz tür de veriyi cachelemek (Asenkron)
        /// </summary>
        /// <typeparam name="TEntity">Cachelenecek tip</typeparam>
        /// <param name="key">Veri tipi</param>
        /// <param name="value">Değer</param>
        /// <param name="absoluteExpirationMinute">Süre</param>
        /// <param name="slidingExpirationSecond">Uzama Süresi</param>
        public async Task SetObjectAsync<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 1, int slidingExpirationSecond = 10)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinute),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSecond)
            };

            var serilazedValue = JsonSerializer.Serialize(value);

            await _distributedCache.SetStringAsync(key, serilazedValue, distributedCacheEntryOptions);
        }

        /// <summary>
        /// Cache silme
        /// </summary>
        /// <param name="key">Cachelenen ad</param>
        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            _distributedCache.Remove(key);
        }

        /// <summary>
        /// Silinecek cache adı (Asenkron)
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        public async Task RemoveAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            await _distributedCache.RemoveAsync(key);
        }

        /// <summary>
        /// Dosya vb. redis cache
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        /// <param name="value">byte değer</param>
        /// <param name="absoluteExpirationMinute">cache süresi</param>
        /// <param name="slidingExpirationSecond">uzama süresi</param>
        public void FileCache(string key, byte[] value, int absoluteExpirationMinute = 10, int slidingExpirationSecond = 0)
        {
            if (string.IsNullOrEmpty(key))
                return;

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinute)
            };

            if (slidingExpirationSecond > 0)
                distributedCacheEntryOptions.SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSecond);

            //_distributedCache.Set(string key, byte[] value);
            _distributedCache.Set(key, value, distributedCacheEntryOptions);
        }

        /// <summary>
        /// Dosya vb. redis cache (Asenkron)
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        /// <param name="value">byte değer</param>
        /// <param name="absoluteExpirationMinute">cache süresi</param>
        /// <param name="slidingExpirationSecond">uzama süresi</param>
        public async Task FileCacheAsync(string key, byte[] value, int absoluteExpirationMinute = 10, int slidingExpirationSecond = 0)
        {
            if (string.IsNullOrEmpty(key))
                return;

            var distributedCacheEntryOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinute)
            };

            if (slidingExpirationSecond > 0)
                distributedCacheEntryOptions.SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSecond);

            //_distributedCache.Set(string key, byte[] value);
            await _distributedCache.SetAsync(key, value, distributedCacheEntryOptions);
        }
    }
}
