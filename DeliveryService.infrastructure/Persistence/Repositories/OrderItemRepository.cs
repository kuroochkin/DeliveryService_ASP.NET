using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class OrderItemRepository : GenericRepository<OrderItemEntity>, IOrderItemRepository
{
	public OrderItemRepository(ApplicationDbContext context) : base(context)
	{
	}
}
