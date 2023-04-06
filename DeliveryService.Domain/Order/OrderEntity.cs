using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.Product;
using DeliveryService.Domain.User;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryService.Domain.Order;

public class OrderEntity
{
	private List<ProductEntity> _products = new();
	public Guid Id { get; }

	public DateTime Created { get; set; }

	public DateTime End { get; set; }

	public string Description { get; set; }

	public enum OrderStatus
	{
		Create,
		Progress,
		Complete,
	};

	[NotMapped]
	public OrderStatus Status { get; set; }
	public OrderStatus GetStatus => Status;

	public string GetStatusOrderToString()
	{
		switch (Status)
		{
			case OrderStatus.Create:
				return "Create";
			case OrderStatus.Progress:
				return "Progress";
			case OrderStatus.Complete:
				return "Complete";
		}

		return "";
	}

	public CourierEntity? Courier { get; set; }

	public CustomerEntity Customer{ get; set; } = null!;

	public IReadOnlyList<ProductEntity> Order => _products.AsReadOnly();

	public OrderEntity()
	{
		Id = Guid.NewGuid();
		Created = DateTime.Now;
	}
}
