using DeliveryService.App.Courier.Queries.GetCourierDetails;
using DeliveryService.Contracts.Courier.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers;

[ApiController]
[Route("api/courier")]
public class CourierController : ApiController
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public CourierController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet("profile")]
	public async Task<IActionResult> GetDetailsOrder()
	{
		var courierId = GetUserId();

		var query = new GetCourierDetailsQuery(courierId);

		var result = await _mediator.Send(query);

		return result.Match(
			courier => Ok(_mapper.Map<GetCourierDetailsResponse>(courier)),
			errors => Problem("Ошибка")
		);
	}
}
