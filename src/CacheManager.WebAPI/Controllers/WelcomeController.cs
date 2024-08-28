using Microsoft.AspNetCore.Mvc;

namespace CacheManager.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WelcomeController(ILogger<WelcomeController> logger) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        var data = new
        {
            DateTime = DateTime.Now,
            Machine = Environment.MachineName,
            Domain = Environment.UserDomainName,
            User = Environment.UserName
        };

        return Ok(data);
    }
}