using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.Product;

namespace DeliveryService.Domain.Order;

public class OrderEntity
{
	private List<ProductEntity> _products = new();
	public Guid Id { get; }

	public DateTime Created { get; set; }

	public DateTime End { get; set; }

	public CourierEntity Courier { get; set; } = null!;

	public CustomerEntity Customer{ get; set; } = null!;

	public IReadOnlyList<ProductEntity> Order => _products.AsReadOnly();

	public OrderEntity(CourierEntity courier, CustomerEntity customer)
	{
		Id = Guid.NewGuid();
		Created = DateTime.Now;
		End = DateTime.Now.AddMinutes(30);
		Courier = courier;
		Customer = customer;
	}
}
