using DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GelAllOrdersByCustomer;
using DeliveryService.App.Product.Queries.GetAllProducts;
using DeliveryService.App.Product.Queries.GetProductDetails;
using DeliveryService.App.Product.Queries.GetProductsBySection;
using DeliveryService.Contracts.Product.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DeliveryService.API.Controllers;

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

	/// <summary>
	/// Получение всех продуктов из всех ресторанов
	/// </summary>
	/// <returns></returns>
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

	/// <summary>
	/// Получение продуктов по определенной секции
	/// </summary>
	/// <param name="sectionId"></param>
	/// <returns></returns>
	[HttpGet("allProducts/{sectionId}")]
	public async Task<IActionResult> GetAllProducts(string sectionId)
	{
		var query = new GetProductsBySectionQuery(sectionId);

		var productResult = await _mediator.Send(query);

		return productResult.Match(
			orders => Ok(_mapper.Map<GetAllProductsResponse>(orders)),
			errors => Problem("Ошибка")
		);
	}

	/// <summary>
	/// Получение информации о конкретном продукте
	/// </summary>
	/// <param name="productId"></param>
	/// <returns></returns>
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
