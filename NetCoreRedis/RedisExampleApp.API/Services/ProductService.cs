namespace RedisExampleApp.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task CreateAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
        }

        public async Task<List<Product>> GetAsync()
        {
            return await _productRepository.GetAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }
    }
}
