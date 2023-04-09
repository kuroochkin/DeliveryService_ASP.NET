using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Order.Queries.GetOrderDetails;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GelAllOrdersByCustomer;

public class GetOrdersCustomerQueryHandler
    : IRequestHandler<GetOrdersCustomerQuery, ErrorOr<OrdersUserVm>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetOrdersCustomerQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ErrorOr<OrdersUserVm>> Handle(
        GetOrdersCustomerQuery request,
        CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.CustomerId, out var customerId))
        {
            return Errors.Customer.InvalidId;
        }

        var customer = await _unitOfWork.Customers.FindById(customerId);
        if (customer is null)
        {
            return Errors.Customer.NotFound;
        }

        var orders = await _unitOfWork.Orders.FindOrdersByCustomerId(customerId);

        var allOrderModel = orders.Select(order => new OrderDetailsVm(
        order.Id.ToString(),
        order.Description,
        order.Created,
        order.End,
        order.Status,
        new CourierVm(
            order.Courier.Id.ToString(),
            order.Courier.LastName,
            order.Courier.FirstName
            ),
        new CustomerVm(
            order.Customer.Id.ToString(),
            order.Customer.LastName,
            order.Customer.FirstName
            )
        )).ToList();

        var allOrdersByCustomer = new OrdersUserVm(allOrderModel);

        return allOrdersByCustomer;
    }
}
