namespace NetCoreRedis.Models.Settings
{
    /// <summary>
    /// Redis Konfigürasyonu
    /// </summary>
    public class RedisSettings
    {
        /// <summary>
        /// Redis Host Adresi
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Redisin çalıştığı port
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// Redise bağlanılmadığı durumda
        /// </summary>
        public bool AbortOnConnectFail { get; set; }

        /// <summary>
        /// Redise async isteklerde belirlediğim saniyeden geç yanıt verilirse timeouta düşmesi için
        /// </summary>
        public int AsyncTimeOutMilliSecond { get; set; }

        /// <summary>
        /// Redise normal isteklerde belirlediğim saniyeden geç yanıt verilirse timeouta düşmesi için
        /// </summary>
        public int ConnectTimeOutMilliSecond { get; set; }
    }
}
