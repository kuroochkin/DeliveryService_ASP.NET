using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.OrderAPI.Domain.Order;
using Microsoft.EntityFrameworkCore;
using static DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
{
	public OrderRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<List<OrderEntity>?> FindOrdersByCourierId(Guid id)
	{
		return await _context.Orders
			.Include(order => order.OrderItems)
			.Where(order => order.CourierId == id)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersByCustomerId(Guid id)
	{
		return await _context.Orders
			.Include(order => order.OrderItems)
			.Where(order => order.CustomerId == id)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersByCustomerIdByOrderStatus(
		Guid id,
		OrderStatus orderStatus)
	{
		return await _context.Orders
			.Include(order => order.OrderItems)
			.Where(order => order.CustomerId == id)
			.Where(order => order.Status == orderStatus)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersByCourierIdByOrderStatus(
		Guid id,
		OrderStatus orderStatus)
	{
		return await _context.Orders
			.Include(order => order.OrderItems)
			.Where(order => order.CourierId == id)
			.Where(order => order.Status == orderStatus)
			.ToListAsync();
	}

	public async Task<OrderEntity?> FindOrderWithCustomerAndCourierAndManager(Guid id)
	{
		return await _context.Orders
			.FirstOrDefaultAsync(order => order.Id == id);
	}
}
