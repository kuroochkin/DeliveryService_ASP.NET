
using DeliveryService.Services.ProductAPI.App.Section.Queries.GetAllSections;
using DeliveryService.Services.ProductAPI.Contracts.Section.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.ProductAPI.Controllers;

[Route("api/sections")]
public class SectionController : Controller
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public SectionController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet("allSections")]
	public async Task<IActionResult> GetAllSections()
	{
		var query = new GetAllSectionsQuery();

		var productResult = await _mediator.Send(query);

		return productResult.Match(
			orders => Ok(_mapper.Map<GetAllSectionsResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}
}
