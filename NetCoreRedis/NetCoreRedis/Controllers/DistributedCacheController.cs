using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Services.Redises;

namespace NetCoreRedis.Controllers
{
    public class DistributedCacheController : Controller
    {
        private readonly IRedisDistributedService _redisDistributedService;
        public DistributedCacheController(IRedisDistributedService redisDistributedService)
        {
            _redisDistributedService = redisDistributedService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
