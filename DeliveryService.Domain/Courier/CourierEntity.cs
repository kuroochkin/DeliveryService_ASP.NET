
using DeliveryService.Domain.Order;
using DeliveryService.Domain.Product;

namespace DeliveryService.Domain.Courier;

public class CourierEntity
{
	private List<OrderEntity> _orders = new();
	public Guid Id { get;}

	public string LastName { get; }

	public string FirstName { get; }

	public string? Patronymic { get; }

	public DateTime BirthDay { get; }

	public int CountOrder { get; set; }

	public IReadOnlyList<OrderEntity> Orders => _orders.AsReadOnly();

	public CourierEntity(string lastName, string firstName, string patronymic)
	{
		Id = Guid.NewGuid();
		FirstName = firstName;
		LastName = lastName;
		Patronymic = patronymic;
		BirthDay = DateTime.Now;
		CountOrder = 0;
	}

	public CourierEntity()
	{

	}

	public void AddOrder(OrderEntity order)
	{
		_orders.Add(order);
		CountOrder++;
	}

	public string GetFullName()
	{
		return Patronymic is null ? $"{LastName} {FirstName}" : $"{LastName} {FirstName} {Patronymic}";
	}

}
