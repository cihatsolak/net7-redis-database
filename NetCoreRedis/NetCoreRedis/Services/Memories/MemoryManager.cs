using Microsoft.Extensions.Caching.Memory;
using System;

namespace NetCoreRedis.Services.Memories
{
    /// <summary>
    /// Memory Manager
    /// </summary>
    public partial class MemoryManager : IMemoryService
    {
        /// <summary>
        /// services.AddMemoryCache(); DI
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        public MemoryManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Veri cache'lemek
        /// </summary>
        /// <typeparam name="TEntity">Veri tipi</typeparam>
        /// <param name="key">Benzersiz ismi</param>
        /// <param name="value">Değer</param>
        /// <param name="slidingExpirationSecond">varsayılan olarak cache e absolute expire süresi içinde erişilirse, 10 saniye daha cache ömrü uzar.</param>
        /// <param name="priority">cache'in önem derecesini ifade eder. Cache dolarsa silinme işlemi bu önem derecesine göre yapılır.</param>
        /// <param name="absoluteExpirationMinute">varsayılan olarak 10 dakika cacheler.</param>
        public void Set<TEntity>(string key, TEntity value, int slidingExpirationSecond = 10, CacheItemPriority priority = CacheItemPriority.Low, int absoluteExpirationMinute = 10)
        {
            if (string.IsNullOrEmpty(key))
                return;

            _memoryCache.Set(key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(absoluteExpirationMinute),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpirationSecond),
                Priority = priority
            });
        }

        /// <summary>
        /// Cache'den veri çekmek için kullanılır
        /// </summary>
        /// <typeparam name="TEntity">Veri tipi</typeparam>
        /// <param name="key">Benzersiz ismi</param>
        /// <returns></returns>
        public TEntity Get<TEntity>(string key)
        {
            if (string.IsNullOrEmpty(key))
                return default;

            return _memoryCache.Get<TEntity>(key);
        }

        /// <summary>
        /// Cache'den veri çağırırken gerçekten var olup olmama durumuna göre ele alırız. False dönüşü yaparsa veri yok demektir.
        /// </summary>
        /// <typeparam name="TEntity">veri Tipi</typeparam>
        /// <param name="key">benzersiz ismi</param>
        /// <param name="value">değer</param>
        /// <returns></returns>
        public bool TryGetValue<TEntity>(string key, out TEntity value)
        {
            //Out: Bir metotda birden fazla değer dönebilmek için kullanılır.
            if (string.IsNullOrEmpty(key))
            {
                value = default;
                return false;
            }

            return _memoryCache.TryGetValue<TEntity>(key, out value);
        }

        /// <summary>
        /// Cache'den silmek
        /// </summary>
        /// <param name="key">Benzersiz ismi</param>
        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            _memoryCache.Remove(key);
        }

        /// <summary>
        /// Cache de böyle bir veri varmı diye bakar, yok ise factory ile belirttiğimiz şekilde oluşturup döner.
        /// </summary>
        /// <typeparam name="TEntity">Veri Tipi</typeparam>
        /// <param name="key">Benzersiz isim</param>
        /// <param name="factory">Dönecek değer</param>
        /// <returns></returns>
        public TEntity GetOrCreate<TEntity>(string key, Func<ICacheEntry, TEntity> factory)
        {
            return _memoryCache.GetOrCreate<TEntity>(key, factory);
        }
    }
}
