using DeliveryService.Services.OrderAPI.Domain.Order;
using static DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity;

namespace DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;

public interface IOrderRepository : IGenericRepository<OrderEntity>
{
	Task<OrderEntity?> FindOrderWithCustomer(Guid id);

	Task<OrderEntity?> FindOrderWithCustomerAndManager(Guid id);

	Task<OrderEntity?> FindOrderWithCustomerAndCourier(Guid id);

	Task<OrderEntity?> FindOrderWithCustomerAndCourierAndManager(Guid id);

	Task<List<OrderEntity>?> FindOrdersByCustomerId(Guid id);

	Task<List<OrderEntity>?> FindOrdersByCourierId(Guid id);

	Task<List<OrderEntity>?> FindOrdersByCustomerIdByOrderStatus(Guid id, OrderStatus orderStatus);

	Task<List<OrderEntity>?> FindOrdersByCourierIdByOrderStatus(Guid id, OrderStatus orderStatus);
}
