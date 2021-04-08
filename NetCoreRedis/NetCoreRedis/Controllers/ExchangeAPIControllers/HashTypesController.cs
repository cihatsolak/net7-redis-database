using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Services.Redises.ExchangeAPI;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreRedis.Controllers.ExchangeAPIControllers
{
    public class HashTypesController : Controller
    {
        private const string LIST_KEY = "hashNames";

        private readonly IDatabase _database;
        public HashTypesController(IRedisStackExchangeAPI redisStackExchangeAPI)
        {
            _database = redisStackExchangeAPI.GetSelectedDatabase(5); //5 numaralı veri tabanını seçiyorum.
        }

        public IActionResult Show()
        {
            Dictionary<string, string> list = new Dictionary<string, string>();

            if (_database.KeyExists(LIST_KEY))
            {
                _database.HashGetAll(LIST_KEY).ToList().ForEach(item =>
                {
                    list.Add(item.Name, item.Value);
                });
            }

            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name, string value)
        {
            await _database.HashSetAsync(LIST_KEY, name, value);
            return RedirectToAction(nameof(Show));
        }

        public async Task<IActionResult> Delete(string name)
        {
            await _database.HashDeleteAsync(LIST_KEY, name);
            return RedirectToAction(nameof(Show));
        }
    }
}
