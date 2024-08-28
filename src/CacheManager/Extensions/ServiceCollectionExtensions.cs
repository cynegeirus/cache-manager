using CacheManager.Services.Abstract;
using CacheManager.Services.Concrete.Redis;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CacheManager.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRedisCache(this IServiceCollection services, string? redisConnectionString)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var configuration = ConfigurationOptions.Parse(redisConnectionString!, true);
            return ConnectionMultiplexer.Connect(configuration);
        });

        services.AddScoped<IGlobalCacheService, RedisCacheService>();

        return services;
    }
}