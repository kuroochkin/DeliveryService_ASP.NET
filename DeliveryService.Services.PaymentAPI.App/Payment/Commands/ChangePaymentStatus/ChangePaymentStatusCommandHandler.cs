using DeliveryService.App.Common.RabbitMQSender;
using DeliveryService.Services.PaymentAPI.App.Common.Errors;
using DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.PaymentAPI.App.Common.Messages;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.PaymentAPI.App.Payment.Commands.ChangePayment;

public class ChangePaymentStatusCommandHandler
	: IRequestHandler<ChangePaymentStatusCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IRabbitMQMessageSender _rabbitMessageSender;

	public ChangePaymentStatusCommandHandler(
		IUnitOfWork unitOfWork,
		IRabbitMQMessageSender rabbitMessageSender)
	{
		_unitOfWork = unitOfWork;
		_rabbitMessageSender = rabbitMessageSender;
	}

	public async Task<ErrorOr<bool>> Handle(
		ChangePaymentStatusCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Payment.InvalidId;
		}

		var payment = await _unitOfWork.Payments.FindPaymentByOrderId(orderId);
		if (payment is null)
		{
			return Errors.Payment.NotFound;
		}

		var changeStatus = new ChangePaymentStatusDTO()
		{
			OrderId = orderId.ToString(),
			PaymentStatus = true
		};

		_rabbitMessageSender.SendMessage(changeStatus, "changeStatusqueue");

		return true;
	}
}
