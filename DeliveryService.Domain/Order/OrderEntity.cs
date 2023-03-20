﻿

using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Product;

namespace DeliveryService.Domain.Order;

public class OrderEntity
{
	private List<ProductEntity> _products = new();
	public Guid Id { get; }

	public DateTime Created { get; set; }

	public DateTime End { get; set; }

	public CourierEntity Courier { get; set; } = null!;

	public IReadOnlyList<ProductEntity> Order => _products.AsReadOnly();
}
