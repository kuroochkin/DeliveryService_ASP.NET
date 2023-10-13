using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Common.RabbitMQSender;
using DeliveryService.App.Order.Commands.CreateOrder;
using DeliveryService.Domain;
using DeliveryService.Domain.Order;
using ErrorOr;
using MediatR;
using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.App.Customer.CreateOrder;

public class CreateOrderCommandHandler
    : IRequestHandler<CreateOrderCommand, ErrorOr<bool>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRabbitMQMessageSender _rabbitMessageSender; 

    public CreateOrderCommandHandler(
        IUnitOfWork unitOfWork, 
        IRabbitMQMessageSender rabbitMessageSender)
    {
        _unitOfWork = unitOfWork;
        _rabbitMessageSender = rabbitMessageSender;
    }

    public async Task<ErrorOr<bool>> Handle(
        CreateOrderCommand request,
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

        var orderItems = request.Products.Select(product => new OrderItemEntity(
            Convert.ToInt32(product.Count),
            double.Parse(product.TotalPrice),
            Convert.ToInt32(product.ProductId),
            product.Thumbnail,
            product.Title
            )).ToList();


        var order = new OrderEntity()
        {
            Description = request.Description,
            Status = OrderStatus.Create,
            Customer = customer,
            OrderItems = orderItems,
        };


        if (await _unitOfWork.Orders.Add(order))
        {
            customer.AddOrder(order);

            return await _unitOfWork.CompleteAsync();
        }

        return false;
    }
}
