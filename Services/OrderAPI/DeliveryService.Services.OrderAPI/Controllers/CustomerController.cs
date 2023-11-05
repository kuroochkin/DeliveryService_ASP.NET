using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.OrderAPI.Controllers;


[ApiController]
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
}