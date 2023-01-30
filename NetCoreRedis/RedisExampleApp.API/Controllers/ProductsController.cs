namespace RedisExampleApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product is null)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            await _productService.CreateAsync(product);
            return Created(string.Empty, product);
        }
    }
}
