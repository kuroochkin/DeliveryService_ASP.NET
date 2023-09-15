using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Courier.GetAllOrdersByCourier;

public class GetOrdersCourierQueryHandler
    : IRequestHandler<GetOrdersCourierQuery, ErrorOr<OrdersUserVm>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersCourierQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<OrdersUserVm>> Handle(
        GetOrdersCourierQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.CourierId, out var courierId))
        {
            return Errors.Courier.InvalidId;
        }

        var courier = await _unitOfWork.Couriers.FindById(courierId);
        if (courier is null)
        {
            return Errors.Customer.NotFound;
        }

        var orders = await _unitOfWork.Orders.FindOrdersByCourierId(courierId);

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
        ).ToList()
        )).ToList();

        var allOrdersByCustomer = new OrdersUserVm(allOrderModel);

        return allOrdersByCustomer;
    }
}
