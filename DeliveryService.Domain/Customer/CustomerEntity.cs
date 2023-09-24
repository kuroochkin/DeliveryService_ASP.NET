using DeliveryService.Domain.Order;

namespace DeliveryService.Domain.Customer;

public class CustomerEntity
{
	private List<OrderEntity> _orders = new();
	
	public Guid Id { get; }

	public string LastName { get; set; }

	public string FirstName { get; set; }

	public string? Patronymic { get; set; }

	public DateTime BirthDay { get; set; }

	public int CountOrder { get; set; }

	public IReadOnlyList<OrderEntity> Orders => _orders.AsReadOnly();


	public CustomerEntity(Guid id, string lastName, string firstName)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
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
