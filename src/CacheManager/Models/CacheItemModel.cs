namespace CacheManager.Models;

public class CacheItemModel<T>
{
    public string? Key { get; set; }
    public T? Value { get; set; }
}