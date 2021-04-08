using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Models;
using NetCoreRedis.Services.Redises.Distributed;
using System;
using System.IO;
using System.Threading.Tasks;

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
            _redisDistributedService.SetObject("time", DateTime.Now, 20, 10);
            return View();
        }

        public async Task<IActionResult> Show()
        {
            DateTime time = await _redisDistributedService.GetObjectAsync<DateTime>("time");
            return View(time);
        }

        public IActionResult ComplexType()
        {
            var vehicle = new Vehicle
            {
                Brand = "Seat",
                Model = "Leon",
                Price = 1000
            };

            //Set
            _redisDistributedService.SetObject("vehicle", vehicle, 5, 60);

            //Get
            var cachedVehicle = _redisDistributedService.GetObject<Vehicle>("vehicle");
            return View(cachedVehicle);
        }

        public async Task<IActionResult> FileCache()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/volkswagen.jpg");
            if (string.IsNullOrEmpty(path))
                return View();

            byte[] ImageByte = System.IO.File.ReadAllBytes(path);

            await _redisDistributedService.FileCacheAsync("image", ImageByte, 5);

            return View();
        }
    }
}
