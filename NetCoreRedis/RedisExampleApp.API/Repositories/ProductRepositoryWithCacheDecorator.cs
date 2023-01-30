namespace RedisExampleApp.API.Repositories
{
    /// <summary>
    /// DI Container'dan 
    /// IProductRepository'i talep edersem --> ProductRepositoryWithCacheDecorator bu class'ı üretir.
    /// ProductRepositoryWithCacheDecorator class içerisinde IProductRepository talep edersem  ProductRepository class'ını üretir.
    /// </summary>
    public class ProductRepositoryWithCacheDecorator : IProductRepository
    {
        private const string productKey = "product-caches";
        private readonly IProductRepository _productRepository;
        private readonly RedisService _rediService;
        private readonly IDatabase _cacheRepository;

        public ProductRepositoryWithCacheDecorator(
            IProductRepository productRepository, 
            RedisService rediService)
        {
            _productRepository = productRepository;
            _rediService = rediService;
            _cacheRepository = _rediService.GetDb(2);
        }

        public async Task CreateAsync(Product product)
        {
            await _productRepository.CreateAsync(product);

            if (_cacheRepository.KeyExists(productKey))
            {
                await _cacheRepository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(product));
            }
        }

        public async Task<List<Product>> GetAsync()
        {
            if (!await _cacheRepository.KeyExistsAsync(productKey))
                return await LoadToCacheFromDbAsync();

            var products = new List<Product>();

            var cacheProducts = await _cacheRepository.HashGetAllAsync(productKey);
            foreach (var item in cacheProducts)
            {
                var product = JsonSerializer.Deserialize<Product>(item.Value);
                products.Add(product);
            }

            return products;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            if (!_cacheRepository.KeyExists(productKey))
            {
                var products = await LoadToCacheFromDbAsync();
                return products.SingleOrDefault(x => x.Id == id);
            }

            var product = await _cacheRepository.HashGetAsync(productKey, id);

            return product.HasValue ? JsonSerializer.Deserialize<Product>(product) : default;
        }

        private async Task<List<Product>> LoadToCacheFromDbAsync()
        {
            var products = await _productRepository.GetAsync();
            
            products.ForEach(product =>
            {
                _cacheRepository.HashSetAsync(productKey, product.Id, JsonSerializer.Serialize(product));

            });

            return products;
        }
    }
}
