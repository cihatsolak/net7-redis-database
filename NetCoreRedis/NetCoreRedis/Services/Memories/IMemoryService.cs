using Microsoft.Extensions.Caching.Memory;
using System;

namespace NetCoreRedis.Services
{
    /// <summary>
    /// Memory Service
    /// </summary>
    public partial interface IMemoryService
    {
        /// <summary>
        /// Veri cache'lemek
        /// </summary>
        /// <typeparam name="TEntity">veri tipi</typeparam>
        /// <param name="key">benzersiz ismi</param>
        /// <param name="value">değer</param>
        /// <param name="slidingExpirationSecond">varsayılan olarak cache e absolute expire süresi içinde erişilirse, 10 saniye daha cache ömrü uzar.</param>
        /// <param name="priority">cache'in önem derecesini ifade eder. Cache dolarsa silinme işlemi bu önem derecesine göre yapılır.</param>
        /// <param name="absoluteExpirationMinute">varsayılan olarak 10 dakika cacheler.</param>
        void Set<TEntity>(string key, TEntity value, int slidingExpirationSecond = 10, CacheItemPriority priority = CacheItemPriority.Low, int absoluteExpirationMinute = 10);

        /// <summary>
        /// Cache'den veri çekmek için kullanılır
        /// </summary>
        /// <typeparam name="TEntity">veri tipi</typeparam>
        /// <param name="key">benzersiz ismi</param>
        /// <returns></returns>
        TEntity Get<TEntity>(string key);

        /// <summary>
        /// Cache'den veri çağırırken gerçekten var olup olmama durumuna göre ele alırız. False dönüşü yaparsa veri yok demektir.
        /// </summary>
        /// <typeparam name="TEntity">veri Tipi</typeparam>
        /// <param name="key">benzersiz ismi</param>
        /// <param name="value">değer</param>
        /// <returns></returns>
        bool TryGetValue<TEntity>(string key, out TEntity value);

        /// <summary>
        /// Cache'den silmek
        /// </summary>
        /// <param name="key">benzersiz ismi</param>
        void Remove(string key);

        /// <summary>
        /// Cache de böyle bir veri varmı diye bakar, yok ise factory ile belirttiğimiz şekilde oluşturup döner.
        /// </summary>
        /// <typeparam name="TEntity">veri Tipi</typeparam>
        /// <param name="key">benzersiz isim</param>
        /// <param name="factory">dönecek değer</param>
        /// <returns></returns>
        TEntity GetOrCreate<TEntity>(string key, Func<ICacheEntry, TEntity> factory);
    }
}
