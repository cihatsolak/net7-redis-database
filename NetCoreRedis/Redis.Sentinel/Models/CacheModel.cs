namespace Redis.Sentinel.Models
{
    public record CacheModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
