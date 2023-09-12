using DeliveryService.Domain.Restaraunt;

namespace DeliveryService.Domain.Manager;

public class ManagerEntity
{
	public Guid Id { get; set; }
	public RestaurantEntity Restaurant { get; set; }
	public string LastName { get; set; }
	public string FirstName { get; set; }
}
