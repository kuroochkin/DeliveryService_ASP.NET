using DeliveryService.App.Customer.Queries.GetCustomerDetails;
using DeliveryService.Contracts.Customer.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ApiController
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public CustomerController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet("profile")]
	public async Task<IActionResult> GetDetailsOrder()
	{
		var customerId = GetUserId();

		var query = new GetCustomerDetailsQuery(customerId);

		var orderResult = await _mediator.Send(query);

		return orderResult.Match(
			order => Ok(_mapper.Map<GetCustomerDetailsResponse>(order)),
			errors => Problem("Ошибка")
		);
	}
}
