using DeliveryService.Services.PaymentAPI.App.Payment.Commands.ChangePayment;
using DeliveryService.Services.PaymentAPI.Contracts.Payment.Requests;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.PaymentAPI.Controllers;

[ApiController]
[Route("api/payment")]
public class PaymentController : ApiController
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public PaymentController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpPost("changeStatus")]
	public async Task<IActionResult> ChangeStatus(ChangePaymentStatusRequest request)
	{
		var command = _mapper.Map<ChangePaymentStatusCommand>(request);

		var result = await _mediator.Send(command);

		return result.Match(
			paymentResult => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}
}
