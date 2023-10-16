using DeliveryService.Services.OrderAPI.Domain.OrderItem;

namespace DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;

public interface IOrderItemRepository : IGenericRepository<OrderItemEntity>
{
}
