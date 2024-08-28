namespace CacheManager.Services.Abstract;

public interface IGlobalCacheService
{
    T? Get<T>(string cacheKey);
    List<KeyValuePair<string, T>> GetAll<T>();
    bool Add<T>(string? cacheKey, T cacheValue);
    bool Delete(string cacheKey);
    bool DeleteAll();
}