using DeliveryService.Services.BasketAPI.Core.Entities;

namespace DeliveryService.Services.BasketAPI.Core.Interfaces;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName);
    Task<ShoppingCart> UpdateBasket(ShoppingCart basket);
    Task<bool> DeleteBasket(string userName);
}
