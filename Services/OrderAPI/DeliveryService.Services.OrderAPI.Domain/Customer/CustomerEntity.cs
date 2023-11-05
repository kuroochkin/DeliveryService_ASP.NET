using DeliveryService.Services.OrderAPI.Domain.Order;

namespace DeliveryService.Services.OrderAPI.Domain.Customer;

public class CustomerEntity
{
	private List<OrderEntity> _orders = new();

	public Guid Id { get; }

	public string LastName { get; set; }

	public string FirstName { get; set; }

	public DateTime BirthDay { get; set; }

	public int CountOrder { get; set; }

	public IReadOnlyList<OrderEntity> Orders => _orders.AsReadOnly();

	public CustomerEntity(Guid id, string lastName, string firstName)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
		BirthDay = DateTime.UtcNow;
		CountOrder = 0;
	}

	public CustomerEntity()
	{

	}

	public void AddOrder(OrderEntity order)
	{
		_orders.Add(order);
		CountOrder++;
	}

	public string GetFullName()
	{
		return $"{LastName} {FirstName}";
	}
}