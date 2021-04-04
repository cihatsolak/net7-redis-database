using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using NetCoreRedis.Services;
using System;

namespace NetCoreRedis.Controllers
{
    public class InMemoryCacheController : Controller
    {
        private readonly IMemoryService _memoryService;

        public InMemoryCacheController(IMemoryService memoryService)
        {
            _memoryService = memoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            _memoryService.Set<DateTime>("time", DateTime.Now);

            return View();
        }

        [HttpGet]
        public IActionResult Sample()
        {
            bool result = _memoryService.TryGetValue<DateTime>("time", out DateTime pastDate);
            if (result)
                return View(pastDate);

            var currentDate = DateTime.Now;
            _memoryService.Set<DateTime>("time", currentDate);

            return View(currentDate);
        }

        [HttpGet]
        public IActionResult Sample2()
        {
            var dateTime = _memoryService.GetOrCreate<DateTime>("time", factory =>
            {
                factory.AbsoluteExpiration = DateTime.Now.AddHours(1);
                factory.SlidingExpiration = TimeSpan.FromMinutes(3);

                return DateTime.Now;
            });

            return View(dateTime);
        }

        [HttpGet]
        public IActionResult Sample3()
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddDays(1),
                SlidingExpiration = TimeSpan.FromMinutes(15),
                Priority = CacheItemPriority.NeverRemove
            };

            //Memory'den silinen verilerin hangi sebeple silindiğini delegate ile ögrenebiliriz.
            options.RegisterPostEvictionCallback((key, value, reason, state) =>
            {
                string data = string.Concat(key, value, "\n", "sebep: ", reason);
                //_memoryCache.Set("callback", data);
            });

            return View();
        }
    }
}
