using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Services.Redises.ExchangeAPI;
using StackExchange.Redis;
using System.Threading.Tasks;

namespace NetCoreRedis.Controllers.ExchangeAPIControllers
{
    public class StringTypesController : Controller
    {
        private readonly IDatabase _database;
        public StringTypesController(IRedisStackExchangeAPI redisStackExchangeAPI)
        {
            _database = redisStackExchangeAPI.GetSelectedDatabase(1); //1 numaralı veri tabanını seçiyorum.
        }

        public async Task<IActionResult> ShowData()
        {
            await _database.StringIncrementAsync("visitor", 10); //Ziyaretçi değerini 10 10 (Increment) arttır.
            _database.StringDecrement("visitor", 5); //ziyaretçi değerini 5 5 azalt.

            await _database.StringGetRangeAsync("name", 0, 3); //name değerini 0 dan başla 3 karakter getir. (substring'le aynı mantık)

            long cachedNameLenght = _database.StringLength("name"); //Verinin uzunluğunu alırız.

            var cachedName = await _database.StringGetAsync("name");
            if (cachedName.HasValue) //Böyle bir data var mı ?
            {
                ViewBag.Value = cachedName;
            }

            return View();
        }

        public async Task<IActionResult> SetIleVeriKaydetme()
        {
            await _database.StringSetAsync("name", "Cihat SOLAK");

            _database.StringSet("visitor", 1000);

            return RedirectToAction(nameof(ShowData));
        }
    }
}
