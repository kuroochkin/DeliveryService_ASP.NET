using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersCourierByStatus;

public class GetOrdersCourierByStatusQueryHandler
	: IRequestHandler<GetOrdersCourierByStatusQuery, ErrorOr<OrdersUserVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOrdersCourierByStatusQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<OrdersUserVm>> Handle(
		GetOrdersCourierByStatusQuery request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CourierId, out var courierId))
		{
			return Errors.Customer.InvalidId;
		}

		OrderStatus orderStatus = (OrderStatus)Enum.Parse(typeof(OrderStatus), request.OrderStatus);

		var courier = await _unitOfWork.Couriers.FindById(courierId);
		if (courier is null)
		{
			return Errors.Courier.InvalidId;
		}

		var orders = await _unitOfWork.Orders.FindOrdersByCourierIdByOrderStatus(
			courierId,
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