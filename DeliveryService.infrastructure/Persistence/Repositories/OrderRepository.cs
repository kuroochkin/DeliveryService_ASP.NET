using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Order;
using Microsoft.EntityFrameworkCore;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class OrderRepository : GenericRepository<OrderEntity>, IOrderRepository
{
	public OrderRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<List<OrderEntity>?> FindOrdersByCourierId(Guid id)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Where(order => order.Courier.Id == id)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersByCustomerId(Guid id)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Include(order => order.OrderItems)
			.Where(order => order.Customer.Id == id)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersByCustomerIdByOrderStatus(
		Guid id,
		OrderStatus orderStatus)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Where(order => order.Customer.Id == id)
			.Where(order => order.Status == orderStatus)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersByCourierIdByOrderStatus(
		Guid id,
		OrderStatus orderStatus)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Where(order => order.Courier.Id == id)
			.Where(order => order.Status == orderStatus)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersCourierByOrderProgress(Guid id)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Where(order => order.Courier.Id == id)
			.Where(order => order.Status == OrderStatus.Progress)
			.ToListAsync();
	}

	public async Task<List<OrderEntity>?> FindOrdersCourierByOrderComplete(Guid id)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Where(order => order.Courier.Id == id)
			.Where(order => order.Status == OrderStatus.Complete)
			.ToListAsync();
	}


	public async Task<List<OrderEntity>?> FindOrdersByCreate()
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Where(order => order.Status == OrderStatus.Create)
			.ToListAsync();
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

	public async Task<OrderEntity?> FindOrderWithCustomerAndCourierAndProducts(Guid id)
	{
		return await _context.Orders
			.Include(order => order.Customer)
			.Include(order => order.Courier)
			.Include(order => order.OrderItems)
			.FirstOrDefaultAsync(order => order.Id == id);
	}
}
