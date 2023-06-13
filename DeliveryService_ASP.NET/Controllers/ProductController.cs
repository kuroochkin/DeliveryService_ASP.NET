﻿using DeliveryService.App.Order.Queries.GetOrdersUser.Customer.GelAllOrdersByCustomer;
using DeliveryService.App.Product.Queries.GetAllProducts;
using DeliveryService.App.Product.Queries.GetProductDetails;
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
