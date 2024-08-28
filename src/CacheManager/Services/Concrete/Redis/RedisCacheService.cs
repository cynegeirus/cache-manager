using CacheManager.Services.Abstract;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CacheManager.Services.Concrete.Redis;

public class RedisCacheService(IConnectionMultiplexer? connectionMultiplexer) : IGlobalCacheService
{
    public T? Get<T>(string cacheKey)
    {
        var cacheDatabase = connectionMultiplexer?.GetDatabase();

        if (cacheDatabase == null)
            return default;

        var value = cacheDatabase.StringGet(cacheKey);
        return value.IsNullOrEmpty ? default : JsonConvert.DeserializeObject<T>(value!);
    }

    public List<KeyValuePair<string, T>> GetAll<T>()
    {
        var cacheServer = connectionMultiplexer?.GetServer(connectionMultiplexer.GetEndPoints().First());
        var cacheKeys = cacheServer?.Keys();
        var cacheDatabase = connectionMultiplexer?.GetDatabase();
        var results = new List<KeyValuePair<string, T?>>();

        if (cacheKeys == null)
            return results;

        results.AddRange(from cacheKey in cacheKeys let value = cacheDatabase!.StringGet(cacheKey) where !value.IsNullOrEmpty let deserializedValue = JsonConvert.DeserializeObject<T>(value!) select new KeyValuePair<string, T?>(cacheKey!, deserializedValue));

        return results;
    }

    public bool Add<T>(string? cacheKey, T cacheValue)
    {
        var cacheDatabase = connectionMultiplexer?.GetDatabase();
        var serializedValue = JsonConvert.SerializeObject(cacheValue, Formatting.Indented);

        return cacheDatabase != null && cacheDatabase.StringSet(cacheKey, serializedValue);
    }

    public bool Delete(string cacheKey)
    {
        var cacheDatabase = connectionMultiplexer?.GetDatabase();
        return cacheDatabase != null && cacheDatabase.KeyDelete(cacheKey);
    }

    public bool DeleteAll()
    {
        var cacheServer = connectionMultiplexer?.GetServer(connectionMultiplexer.GetEndPoints().First());
        var cacheKeys = cacheServer?.Keys();
        var cacheDatabase = connectionMultiplexer?.GetDatabase();
        if (cacheKeys == null)
            return false;

        foreach (var key in cacheKeys) cacheDatabase?.KeyDelete(key);

        return true;
    }
}