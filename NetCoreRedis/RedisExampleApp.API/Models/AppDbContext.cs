namespace RedisExampleApp.API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product[]
            {
                new Product 
                {
                    Id = 1,
                    Name = "Pencil 1"
                },
                new Product
                {
                    Id = 2,
                    Name = "Pencil 2"
                },
                new Product
                {
                    Id = 3,
                    Name = "Pencil 3"
                },
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
    }
}
