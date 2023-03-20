using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Order;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
{
	public OrderRepository(ApplicationDbContext context) : base(context)
	{
	}
}
