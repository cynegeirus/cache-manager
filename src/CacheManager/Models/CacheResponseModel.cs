namespace CacheManager.Models;

public class CacheResponseModel<T>
{
    public string? Machine { get; set; }
    public List<CacheItemModel<T>>? Items { get; set; } = [];
}