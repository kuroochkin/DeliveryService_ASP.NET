using DeliveryService.App.Customer.Commands.CreateCustomer;
using DeliveryService.Contracts.Customer;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers
{
	[Route("api/customer")]
	public class CustomerController : Controller
	{
		private readonly ISender _mediator;
		private readonly IMapper _mapper;

		public CustomerController(ISender mediator, IMapper mapper)
		{
			_mediator = mediator;
			_mapper = mapper;
		}

		[HttpPost("create")]
		public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request)
		{
			var command = _mapper.Map<СreateCustomerCommand>(request);

			var result = await _mediator.Send(command);

			return result.Match(
				coursesResult => Ok(result.Value),
				errors => Problem("Ошибка")
				);
		}
	}
}
