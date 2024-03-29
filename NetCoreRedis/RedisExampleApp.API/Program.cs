var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("TestDB");
});

builder.Services.AddSingleton<RedisService>(provider =>
{
    return new RedisService(builder.Configuration["CacheOptions:Url"]);
});

builder.Services.AddSingleton<IDatabase>(provider =>
{
    var redisService = provider.GetRequiredService<RedisService>();
    return redisService.GetDb(0);
});

builder.Services.AddScoped<IProductRepository>(provider =>
{
    var appDbContext = provider.GetRequiredService<AppDbContext>();
    var redisService = provider.GetRequiredService<RedisService>();
    var logger = provider.GetRequiredService<ILogger<ProductRepositoryWithLogDecorator>>();

    var productRepository = new ProductRepository(appDbContext);
    var productRepositoryWithCacheDecorator = new ProductRepositoryWithCacheDecorator(productRepository, redisService);
    var productRepositoryWithLogDecorator = new ProductRepositoryWithLogDecorator(logger, productRepositoryWithCacheDecorator);

    return productRepositoryWithLogDecorator;
});

builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

using (var serviceScope = app.Services.CreateScope())
{
    var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
    appDbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
