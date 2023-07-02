using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetOrdersCourierByStatus;

public class GetOrdersCourierProgressQueryHandler
	: IRequestHandler<GetOrdersCourierProgressQuery, ErrorOr<OrdersUserVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOrdersCourierProgressQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<OrdersUserVm>> Handle(
		GetOrdersCourierProgressQuery request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CourierId, out var courierId))
		{
			return Errors.Customer.InvalidId;
		}

		var courier = await _unitOfWork.Couriers.FindById(courierId);
		if (courier is null)
		{
			return Errors.Courier.InvalidId;
		}

		var orders = await _unitOfWork.Orders.FindOrdersCourierByOrderProgress(courierId);

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
				   product.TotalPrice.ToString(),
				   product.Thumbnail,
				   product.Title
				   )).ToList()
		   ).ToList())).ToList();

		var allOrdersByCustomer = new OrdersUserVm(allOrderModel);

		return allOrdersByCustomer;
	}
}