using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Services.Redises.ExchangeAPI;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreRedis.Controllers.ExchangeAPIControllers
{
    public class ListTypesController : Controller
    {
        private readonly IDatabase _database;
        public ListTypesController(IRedisStackExchangeAPI redisStackExchangeAPI)
        {
            _database = redisStackExchangeAPI.GetSelectedDatabase(2); //2 numaralı veri tabanını seçiyorum.
        }

        public IActionResult Show()
        {
            List<string> names = new List<string>();

            if (_database.KeyExists("names")) //veritabanın da key var mı?
            {
                _database.ListRange("names").ToList().ForEach(redisValueName =>
                {
                    if (redisValueName.HasValue)
                        names.Add(redisValueName.ToString());
                });
            }

            return View(names);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string name)
        {
            await _database.ListLeftPushAsync("names", name); //Listenin başına name'i ekleyecek.
            //await database.ListRightPushAsync("names", name); //Listenin sonuna name'i ekleyecek.

            return RedirectToAction(nameof(Show));
        }

        public async Task<IActionResult> Delete(string name)
        {
            await _database.ListRemoveAsync("names", name);

            return RedirectToAction(nameof(Show));
        }

        public IActionResult DeleteFirstItem()
        {
            _database.ListLeftPopAsync("names").Wait(); //async await yerine kullanılabilir. Listenin başındaki elemanı siler..
            //database.ListRightPopAsync(listKey).Wait(); //async await yerine kullanılabilir. Listenin sonundaki elemanı siler..

            return RedirectToAction(nameof(Show));
        }
    }
}
