using DeliveryService.Domain.Order;
using DeliveryService.Domain.Restaraunt;

namespace DeliveryService.Domain.Manager;

public class ManagerEntity
{
	public Guid Id { get; set; }
	public RestaurantEntity? Restaurant { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public int CountOrder { get; set; }

	private List<OrderEntity> _orders = new();

	public IReadOnlyList<OrderEntity> Orders => _orders.AsReadOnly();

	public ManagerEntity(Guid id, string lastName, string firstName)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
		CountOrder = 0;
	}
}
