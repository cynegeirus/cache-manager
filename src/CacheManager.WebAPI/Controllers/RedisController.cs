using CacheManager.Constants;
using CacheManager.Models;
using CacheManager.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CacheManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RedisController(IGlobalCacheService cacheService) : ControllerBase
{
    [HttpGet("Get")]
    public IActionResult Get(string key)
    {
        var value = cacheService.Get<string>(key);
        if (value == null)
            return BadRequest(new CacheResponseModel
            {
                Success = false,
                Message = Messages.CacheNotFound
            });

        return Ok(new CacheResponseModel<string>
        {
            Machine = $"{Environment.MachineName} - ({Environment.UserDomainName} - {Environment.UserName})",
            Items = new List<CacheItemModel<string>>
            {
                new()
                {
                    Key = key,
                    Value = value
                }
            }
        });
    }

    [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var pairs = cacheService.GetAll<string>();
        var cacheResponseModel = new CacheResponseModel<string>
        {
            Machine = $"{Environment.MachineName} - ({Environment.UserDomainName} - {Environment.UserName})"
        };

        foreach (var pair in pairs)
            cacheResponseModel.Items?.Add(new CacheItemModel<string>
            {
                Key = pair.Key,
                Value = pair.Value
            });

        return Ok(cacheResponseModel);
    }

    [HttpPost("Add")]
    public IActionResult Add([FromBody] CacheItemModel<string>? item)
    {
        if (item == null) return BadRequest();

        var result = cacheService.Add(item.Key, item.Value);

        return result
            ? Ok(new CacheResponseModel
            {
                Success = result,
                Message = Messages.CacheAdded
            })
            : BadRequest(new CacheResponseModel
            {
                Success = result,
                Message = Messages.TransactionError
            });
    }

    [HttpPost("Delete")]
    public IActionResult Delete(string key)
    {
        var result = cacheService.Delete(key);

        return result
            ? Ok(new CacheResponseModel
            {
                Success = result,
                Message = Messages.CacheDeleted
            })
            : BadRequest(new CacheResponseModel
            {
                Success = result,
                Message = Messages.TransactionError
            });
    }

    [HttpPost("DeleteAll")]
    public IActionResult DeleteAll()
    {
        var result = cacheService.DeleteAll();
        return result
            ? Ok(new CacheResponseModel
            {
                Success = result,
                Message = Messages.TransactionSuccess
            })
            : BadRequest(new CacheResponseModel
            {
                Success = result,
                Message = Messages.TransactionError
            });
    }
}