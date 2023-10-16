using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.OrderAPI.Domain.OrderItem;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence.Repositories;

public class OrderItemRepository : GenericRepository<OrderItemEntity>, IOrderItemRepository
{
	public OrderItemRepository(ApplicationDbContext context) : base(context)
	{
	}
}
