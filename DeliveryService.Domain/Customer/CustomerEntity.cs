
using DeliveryService.Domain.Order;

namespace DeliveryService.Domain.Customer;

public class CustomerEntity
{
	private List<OrderEntity> _orders = new();
	
	public Guid Id { get; }

	public string LastName { get; }

	public string FirstName { get; }

	public string? Patronymic { get; }

	public DateTime BirthDay { get; }

	public int CountOrder { get; set; }

	public IReadOnlyList<OrderEntity> Orders => _orders.AsReadOnly();

	public CustomerEntity(string lastName, string firstName, string patronymic)
	{
		Id = Guid.NewGuid();
		FirstName = firstName;
		LastName = lastName;
		Patronymic = patronymic;
		BirthDay = DateTime.Now;
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
		return Patronymic is null ? $"{LastName} {FirstName}" : $"{LastName} {FirstName} {Patronymic}";
	}
}
