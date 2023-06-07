using DeliveryService.Domain.Order;
using DeliveryService.Domain.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain;

public class OrderItemEntity
{
	public Guid Id { get; set; }
	public int Count { get; set; }

	public double TotalPrice { get; set; }

	[ForeignKey("ProductId")]
	public ProductEntity Product { get; set; }

	public int ProductId { get; set; }

	[ForeignKey("OrderId")]
	public OrderEntity Order { get; set; }

	public Guid OrderId { get; set; }

	public OrderItemEntity(int count, double totalPrice, int productId)
	{
		Id = new Guid();
		Count = count;
		TotalPrice = totalPrice;
		ProductId = productId;
	}
}
