using DeliveryService.App.Common.RabbitMQSender;
using DeliveryService.Services.PaymentAPI.Messages;
using DeliveryService.Services.PaymentAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.PaymentAPI.Contollers;

[ApiController]
[Route("api/payment")]
public class PaymentController : Controller
{
	private readonly IPaymentRepository _paymentRepository;
	private readonly IRabbitMQOrderMessageSender _rabbitMessageSender;
	public PaymentController(
		IPaymentRepository paymentRepository, 
		IRabbitMQOrderMessageSender rabbitMessageSender)
	{
		_paymentRepository = paymentRepository;
		_rabbitMessageSender = rabbitMessageSender;
	}

	[HttpPost("changeStatus/{orderId}")]
	public async Task<IActionResult> ChangeStatus(string orderId)
	{
		var payment = await _paymentRepository.FindPaymentByOrderId(orderId);
		if (payment is null)
			return Problem();

		var changeStatus = new ChangeOrderPaymentStatusDTO()
		{
			OrderId = orderId,
			PaymentStatus = true
		};

		_rabbitMessageSender.SendMessage(changeStatus, "changeStatusqueue");

		return Ok();
	}
}
