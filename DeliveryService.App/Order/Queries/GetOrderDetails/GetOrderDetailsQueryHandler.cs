using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using Minio;

namespace DeliveryService.App.Order.Queries.GetOrderDetails;

public class GetOrderDetailsQueryHandler
	: IRequestHandler<GetOrderDetailsQuery, ErrorOr<OrderDetailsVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetOrderDetailsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<OrderDetailsVm>> Handle
		(GetOrderDetailsQuery request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.OrderId, out var orderId)) 
		{
			return Errors.Order.InvalidId;
		}

		var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourierAndProducts(orderId);
		if (order is null)
		{
			return Errors.Order.NotFound;
		}

		var orderInfo = new OrderDetailsVm(
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
			new CustomerVm(
				order.Customer.Id.ToString(),
				order.Customer.LastName,
				order.Customer.FirstName
				),
			new List<ProductOrderVm>(
				order.OrderItems.Select(product => new ProductOrderVm(
					product.ProductId.ToString(),
					product.Count.ToString(),
					product.TotalPrice.ToString(),
					product.Thumbnail,
					product.Title
			)).ToList()));

		return orderInfo;	
	}
}
