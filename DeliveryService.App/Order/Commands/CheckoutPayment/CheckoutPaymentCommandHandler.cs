using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Common.Messages;
using DeliveryService.App.Common.RabbitMQSender;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.CheckoutPayment;

public class CheckoutPaymentCommandHandler
	: IRequestHandler<CheckoutPaymentCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRabbitMQMessageSender _rabbitMessageSender;

	public CheckoutPaymentCommandHandler(
		IUnitOfWork unitOfWork, 
		IRabbitMQMessageSender rabbitMessageSender)
	{
		_unitOfWork = unitOfWork;
		_rabbitMessageSender = rabbitMessageSender;
	}

	public async Task<ErrorOr<bool>> Handle(
		CheckoutPaymentCommand request,
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Order.InvalidId;
		}

		var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourierAndManager(orderId);
		if (order is null)
		{
			return Errors.Order.NotFound;
		}

		var user = await _unitOfWork.Users.FindById(order.Customer.Id);
		if (user is null)
		{
			return Errors.User.NotFound;
		}

		var checkout = new CheckoutPaymentDTO()
		{
			OrderId = order.Id.ToString(),
			UserId = user.Id.ToString(),
			FirstName = user.FirstName,
			LastName = user.LastName,
			Email = user.Email,
			CardNumber = request.CardNumber,
			Cvv = request.Cvv,
			ExpiryMonthYear = request.ExpiryMonthYear,
			OrderTotalSum = request.OrderTotalSum,
			CartTotalItems = request.CartTotalItems,
		};

		//_rabbitMessageSender.SendMessage(checkout, "checkoutqueue");

		return true;
	}
}
