using DeliveryService.App.Product.Queries.GetAllProducts;
using DeliveryService.App.Section.Queries.GetAllSection;
using DeliveryService.Contracts.Section.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers;

[ApiController]
[Route("api/sections")]
public class SectionController : ApiController
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
		var query = new GetAllSectionQuery();

		var productResult = await _mediator.Send(query);

		return productResult.Match(
			orders => Ok(_mapper.Map<GetAllSectionsResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}
}
