using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Order;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
{
	public OrderRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<OrderEntity?> FindOrderWithCustomer(Guid id)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.FirstOrDefaultAsync(order => order.Id == id);
	}

	public async Task<OrderEntity?> FindOrderWithCustomerAndCourier(Guid id)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.FirstOrDefaultAsync(order => order.Id == id);
	}
}
