using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GetOrdersByCustomerByStatus;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersByCourierByStatus;

public class GetOrdersCourierStatusQueryHandler
	: IRequestHandler<GetOrdersCourierStatusQuery, ErrorOr<OrdersUserVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOrdersCourierStatusQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<OrdersUserVm>> Handle(
		GetOrdersCourierStatusQuery request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CourierId, out var courierId))
		{
			return Errors.Courier.InvalidId;
		}

		OrderStatus orderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), request.OrderStatus);

		var courier = await _unitOfWork.Couriers.FindById(courierId);
		if (courier is null)
		{
			return Errors.Courier.InvalidId;
		}

		var orders = await _unitOfWork.Orders.FindOrdersByCourierIdByOrderStatus(courierId, orderStatus);

		var allOrderModel = orders.Select(order => new OrderDetailsVm(
		   order.Id.ToString(),
		   order.Description,
		   order.Created,
		   order.ConfirmedRestaurant,
		   order.EndRestaurant,
		   order.ConfirmedCourier,
		   order.End,
		   order.Status,
		   new CourierVm(
			   order?.Courier?.Id.ToString(),
			   order?.Courier?.LastName,
			   order?.Courier?.FirstName
			   ),
		   new ManagerVm(
				order?.Manager?.Id.ToString(),
				order?.Manager?.LastName,
				order?.Manager?.FirstName,
				order?.Manager?.Restaurant?.Name),
		   new CustomerVm(
			   order.Customer.Id.ToString(),
			   order.Customer.LastName,
			   order.Customer.FirstName
			   ),
		   new List<ProductOrderVm>(
			   order.OrderItems.Select(product => new ProductOrderVm(
				   product.Id.ToString(),
				   product.Count.ToString(),
				   product.TotalPrice.ToString(),
				   product.Thumbnail,
				   product.Title
				   )).ToList()
		   ).ToList())).ToList();

		var allOrdersByCustomer = new OrdersUserVm(allOrderModel);

		return allOrdersByCustomer;
	}
}
