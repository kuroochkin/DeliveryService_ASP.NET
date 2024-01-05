using System.Configuration;
using DeliveryService.Services.BasketAPI.Core.Interfaces;
using DeliveryService.Services.BasketAPI.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace DeliveryService.Services.BasketAPI.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
         IConfiguration configuration)
    {
        services.AddRedis(configuration);
        return services;
    }

    public static IServiceCollection AddRedis(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionMultiplexer>(sp =>
             ConnectionMultiplexer.Connect(new ConfigurationOptions
             {
                 EndPoints = { configuration["CacheSettings:ConnectionString"] },
                 AbortOnConnectFail = false,
             }));

        services.AddScoped<IBasketRepository, BasketRepository>();

        return services;
    }
}
