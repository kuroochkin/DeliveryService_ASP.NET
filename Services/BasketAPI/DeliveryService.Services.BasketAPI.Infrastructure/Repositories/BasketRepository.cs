using DeliveryService.Services.BasketAPI.Core.Entities;
using DeliveryService.Services.BasketAPI.Core.Interfaces;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace DeliveryService.Services.BasketAPI.Infrastructure.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer connectionMultiplexer)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _database = _connectionMultiplexer.GetDatabase();
    }

    public async Task<bool> DeleteBasket(string userName)
    {
        return await _database.KeyDeleteAsync(userName);
    }

    public async Task<ShoppingCart> GetBasket(string userName)
    {
        var basketString = await _database.StringGetAsync(userName);

        if (basketString.HasValue)
        {
            return JsonConvert.DeserializeObject<ShoppingCart>(basketString.ToString());
        }

        return new ShoppingCart(userName);
    }

    public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
    {
        var result = await _database.StringSetAsync(basket.UserName, JsonConvert.SerializeObject(basket));
        if (result)
            return await GetBasket(basket.UserName);

        return basket;
    }
}
