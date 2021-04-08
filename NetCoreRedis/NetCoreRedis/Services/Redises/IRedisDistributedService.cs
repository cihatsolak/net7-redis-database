using System.Threading.Tasks;

namespace NetCoreRedis.Services.Redises
{
    /// <summary>
    /// Redis Server: IDistributedCache
    /// Nuget Package Manager: Microsoft.Extensions.Caching.StackExchangeRedis
    /// </summary>
    public interface IRedisDistributedService
    {
        /// <summary>
        /// Cachelediğimiz veriyi çağırmak
        /// </summary>
        /// <typeparam name="TEntity">Veri tipi</typeparam>
        /// <param name="key">Cachelenilen ad</param>
        /// <returns></returns>
        TEntity GetObject<TEntity>(string key);

        /// <summary>
        /// Cachelediğimiz veriyi çağırmak (Asenkron)
        /// </summary>
        /// <typeparam name="TEntity">Veri tipi</typeparam>
        /// <param name="key">Cachelenilen ad</param>
        /// <returns></returns>
        Task<TEntity> GetObjectAsync<TEntity>(string key);

        /// <summary>
        /// İstediğimiz tür de veriyi cachelemek
        /// </summary>
        /// <typeparam name="TEntity">Cache'lenecek tip</typeparam>
        /// <param name="key">Veri tipi</param>
        /// <param name="value">Değer</param>
        /// <param name="absoluteExpirationMinute">Süre</param>
        /// <param name="slidingExpirationSecond">Uzama Süresi</param>
        void SetObject<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 1, int slidingExpirationSecond = 10);

        /// <summary>
        /// İstediğimiz tür de veriyi cachelemek (Asenkron)
        /// </summary>
        /// <typeparam name="TEntity">Cachelenecek tip</typeparam>
        /// <param name="key">Veri tipi</param>
        /// <param name="value">Değer</param>
        /// <param name="absoluteExpirationMinute">Süre</param>
        /// <param name="slidingExpirationSecond">Uzama Süresi</param>
        Task SetObjectAsync<TEntity>(string key, TEntity value, int absoluteExpirationMinute = 1, int slidingExpirationSecond = 10);

        /// <summary>
        /// Cache silme
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        void Remove(string key);

        /// <summary>
        /// Silinecek cache adı (Asenkron)
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        Task RemoveAsync(string key);

        /// <summary>
        /// Dosya vb. redis cache
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        /// <param name="value">byte değer</param>
        /// <param name="absoluteExpirationMinute">cache süresi</param>
        /// <param name="slidingExpirationSecond">uzama süresi</param>
        void FileCache(string key, byte[] value, int absoluteExpirationMinute = 10, int slidingExpirationSecond  = 0);

        /// <summary>
        /// Dosya vb. redis cache (Asenkron)
        /// </summary>
        /// <param name="key">cachelenen ad</param>
        /// <param name="value">byte değer</param>
        /// <param name="absoluteExpirationMinute">cache süresi</param>
        /// <param name="slidingExpirationSecond">uzama süresi</param>
        Task FileCacheAsync(string key, byte[] value, int absoluteExpirationMinute = 10, int slidingExpirationSecond = 0);
    }
}
