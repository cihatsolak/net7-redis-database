namespace RedisExampleApp.API.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAsync();
        Task<Product> GetByIdAsync(int id);
        Task CreateAsync(Product product);
    }
}
