using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.RabbitMQSender;
using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.OrderAPI.Domain.Order;
using DeliveryService.Services.OrderAPI.Domain.OrderItem;
using ErrorOr;
using MediatR;
using static DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity;

namespace DeliveryService.Services.OrderAPI.App.Order.Commands.CreateOrder;

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
			CustomerId = customerId,
			OrderItems = orderItems,
		};

		//Добавить отправку сообщений в сервис оплаты!!!!


		if (await _unitOfWork.Orders.Add(order))
		{
			customer.AddOrder(order);

			return await _unitOfWork.CompleteAsync();
		}

		return false;
	}

}
