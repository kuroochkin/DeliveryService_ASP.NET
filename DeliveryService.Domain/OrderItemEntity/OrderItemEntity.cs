using DeliveryService.Domain.Order;
using DeliveryService.Domain.Product;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain;

public class OrderItemEntity
{
	public Guid Id { get; set; }
	public int Count { get; set; }

	public double TotalPrice { get; set; }

	public string Title { get; set; }

	public string Thumbnail { get; set; }

	[ForeignKey("ProductId")]
	public ProductEntity Product { get; set; }

	public int ProductId { get; set; }

	[ForeignKey("OrderId")]
	public OrderEntity Order { get; set; }

	public Guid OrderId { get; set; }

	public OrderItemEntity(int count, double totalPrice, int productId, string thumbnail, string title)
	{
		Id = new Guid();
		Count = count;
		TotalPrice = totalPrice;
		ProductId = productId;
		Title = title;
		Thumbnail = thumbnail;
	}
}
