using Microsoft.AspNetCore.Mvc;
using NetCoreRedis.Services.Redises.ExchangeAPI;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreRedis.Controllers.ExchangeAPIControllers
{
    public class SetTypesController : Controller
    {
        private const string LIST_KEY = "setNames";

        private readonly IDatabase _database;
        public SetTypesController(IRedisStackExchangeAPI redisStackExchangeAPI)
        {
            _database = redisStackExchangeAPI.GetSelectedDatabase(3); //3 numaralı veri tabanını seçiyorum.
        }

        public IActionResult Show()
        {
            HashSet<string> nameList = new HashSet<string>();

            if (_database.KeyExists(LIST_KEY))
            {
                _database.SetMembers(LIST_KEY).ToList().ForEach(p =>
                {
                    nameList.Add(p);
                });
            }

            return View(nameList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            /*
             * Şimdi burada if içerisine almazsam eğer, add metotu her çalıştıgında expire süresi uzayacağından dolayı aslında slidingExpire özellliği kazandırmış oluyoruz.
             * İf içine aldığımızda sadece ilk oluşturma evresinde expire belirleyip sonrasında if içerisinde girmediğinden dolayı sadece absolute expire belirlemiş olacağız.
             */
            if (!_database.KeyExists(LIST_KEY)) //Veri tabanında yoksa Expire belirliyorum. Her istek geldiğinde yenilenmemesi için if 'e aldım.
            {
                _database.KeyExpire(LIST_KEY, DateTime.Now.AddMinutes(5));
            }

            _database.SetAdd(LIST_KEY, name);

            return RedirectToAction(nameof(Show));
        }

        public async Task<IActionResult> Delete(string name)
        {
            await _database.SetRemoveAsync(LIST_KEY, name);

            return RedirectToAction(nameof(Show));
        }
    }
}