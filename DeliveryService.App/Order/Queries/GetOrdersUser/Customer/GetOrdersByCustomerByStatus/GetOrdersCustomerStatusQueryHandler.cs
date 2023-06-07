using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GetOrdersByCustomerByStatus;

public class GetOrdersCustomerStatusQueryHandler
	: IRequestHandler<GetOrdersCustomerStatusQuery, ErrorOr<OrdersUserVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOrdersCustomerStatusQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<OrdersUserVm>> Handle(
		GetOrdersCustomerStatusQuery request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CustomerId, out var customerId))
		{
			return Errors.Customer.InvalidId;
		}

		OrderStatus orderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), request.OrderStatus);

		var customer = await _unitOfWork.Customers.FindById(customerId);
		if (customer is null)
		{
			return Errors.Customer.NotFound;
		}

		var orders = await _unitOfWork.Orders.FindOrdersByCustomerIdByOrderStatus(
			customerId,
			orderStatus);

		var allOrderModel = orders.Select(order => new OrderDetailsVm(
		   order.Id.ToString(),
		   order.Description,
		   order.Created,
		   order.End,
		   order.Status,
		   new CourierVm(
			   order?.Courier?.Id.ToString(),
			   order?.Courier?.LastName,
			   order?.Courier?.FirstName
			   ),
		   new CustomerVm(
			   order.Customer.Id.ToString(),
			   order.Customer.LastName,
			   order.Customer.FirstName
			   ),
		   new List<ProductOrderVm>(
			   order.OrderItems.Select(product => new ProductOrderVm(
				   product.Id.ToString(),
				   product.Count.ToString(),
				   product.TotalPrice.ToString()
				   )).ToList()
		   ).ToList())).ToList();

		var allOrdersByCustomer = new OrdersUserVm(allOrderModel);

		return allOrdersByCustomer;
	}
}
