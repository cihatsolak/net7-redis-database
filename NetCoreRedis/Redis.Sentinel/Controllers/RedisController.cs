using Microsoft.AspNetCore.Mvc;
using Redis.Sentinel.Models;

namespace Redis.Sentinel.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RedisController : ControllerBase
{
    [HttpGet]
    private IActionResult GetValue(string key)
    {
        return Ok();
    }

    [HttpPost]
    private IActionResult SetValue([FromBody] CacheModel cacheModel)
    {
        return Ok();
    }
}
