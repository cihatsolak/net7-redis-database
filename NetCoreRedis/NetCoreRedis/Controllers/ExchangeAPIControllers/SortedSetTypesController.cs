using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Services.Redises.ExchangeAPI;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreRedis.Controllers.ExchangeAPIControllers
{
    public class SortedSetTypesController : Controller
    {
        private const string LIST_KEY = "sortedSetNames";

        private readonly IDatabase _database;
        public SortedSetTypesController(IRedisStackExchangeAPI redisStackExchangeAPI)
        {
            _database = redisStackExchangeAPI.GetSelectedDatabase(4); //4 numaralı veri tabanını seçiyorum.
        }

        [HttpGet]
        public IActionResult Show()
        {
            HashSet<string> sortedSetList = new HashSet<string>();

            if (_database.KeyExists(LIST_KEY)) //Key'e ait veri varsa.
            {
                _database.SortedSetScan(LIST_KEY).ToList().ForEach(item =>
                {
                    sortedSetList.Add(item.ToString());
                });
            }

            /* Cahce den veriyi alırken 0 ile 5 score değerleri arasındaki verileri descending sıralayarak getir. */

            //database.SortedSetRangeByRank(listKey, 0, 5, order: Order.Descending).ToList().ForEach(item =>
            // {
            //     sortedSetList.Add(item.ToString());

            // });

            return View(sortedSetList);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            _database.KeyExpire(LIST_KEY, DateTime.Now.AddMinutes(1)); //Cache süresini belirleme. 1 dakika.
            _database.SortedSetAddAsync(LIST_KEY, name, score).Wait();

            return RedirectToAction(nameof(Show));
        }


        public async Task<IActionResult> Delete(string name)
        {
            await _database.SortedSetRemoveAsync(LIST_KEY, name);

            return RedirectToAction(nameof(Show));
        }
    }
}
