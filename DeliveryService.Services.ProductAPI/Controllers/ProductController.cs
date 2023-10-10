using DeliveryService.Services.ProductAPI.App.Product.Queries.GetAllProducts;
using DeliveryService.Services.ProductAPI.App.Product.Queries.GetProductsBySection;
using DeliveryService.Services.ProductAPI.App.Product.Queries.GetProductsDetails;
using DeliveryService.Services.ProductAPI.Contracts.Product.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.ProductAPI.Controllers;

[ApiController]
[Route("api/product")]
public class ProductController : Controller
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public ProductController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	[HttpGet("allProducts")]
	public async Task<IActionResult> GetAllProducts()
	{
		var query = new GetAllProductsQuery();

		var productResult = await _mediator.Send(query);

		return productResult.Match(
			orders => Ok(_mapper.Map<GetAllProductsResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	[HttpGet("allProducts/{sectionId}")]
	public async Task<IActionResult> GetAllProductsBySection(string sectionId)
	{
		var query = new GetProductsBySectionQuery(sectionId);

		var productResult = await _mediator.Send(query);

		return productResult.Match(
			orders => Ok(_mapper.Map<GetAllProductsResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	[HttpGet("{productId}")]
	public async Task<IActionResult> GetProductById(string productId)
	{
		var query = new GetProductsDetailsQuery(productId);

		var productResult = await _mediator.Send(query);

		return productResult.Match(
			product => Ok(_mapper.Map<GetProductDetailsResponse>(product)),
			errors => Problem("Ошибка")
		);
	}
}
