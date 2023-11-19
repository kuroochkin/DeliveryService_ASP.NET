using DeliveryService.Services.RestaurantAPI.Domain.Restaurant;

namespace DeliveryService.Services.RestaurantAPI.Domain.Manager;

public class ManagerEntity
{
	public Guid Id { get; set; }
	public RestaurantEntity? Restaurant { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
	public int CountOrder { get; set; }

	public ManagerEntity(Guid id, string lastName, string firstName)
	{
		Id = id;
		FirstName = firstName;
		LastName = lastName;
		CountOrder = 0;
	}

	public void AddOrder() => CountOrder++;
}
