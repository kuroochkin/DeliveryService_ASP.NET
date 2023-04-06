using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Order;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IOrderRepository : IGenericRepository<OrderEntity>
{
	Task<OrderEntity?> FindOrderWithCustomer(Guid id);

	Task<OrderEntity?> FindOrderWithCustomerAndCourier(Guid id);

	Task<List<OrderEntity>?> FindOrdersByCustomerId(Guid id);

	Task<List<OrderEntity>?> FindOrdersByCourierId(Guid id);



}
