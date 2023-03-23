using DeliveryService.App.Courier.Commands.СreateCourier;
using DeliveryService.Contracts.Courier;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers
{
	[Route("api/courier")]
	public class CourierController : Controller
	{
		private readonly ISender _mediator;
		private readonly IMapper _mapper;

		public CourierController(ISender mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateCourier(CreateCourierRequest request)
		{
			var command = _mapper.Map<СreateCourierCommand>(request);

			var result = await _mediator.Send(command);

			return result.Match(
				coursesResult => Ok(result.Value),
				errors => Problem("Ошибка")
				);
		}
	}
}
