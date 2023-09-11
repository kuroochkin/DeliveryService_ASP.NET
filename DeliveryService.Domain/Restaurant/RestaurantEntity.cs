using DeliveryService.Domain.Order;
using DeliveryService.Domain.Product;

namespace DeliveryService.Domain.Restaraunt;

public class RestaurantEntity
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public string? Address { get; set; }

	public IsOpen Status { get; set; }

	public List<ProductEntity> Products { get; set; } = new();

	public List<OrderEntity> Orders { get; set; } = new();

	public IsOpen GetStatus => Status;

	public string GetStatusIsOpenToString()
	{
		switch (Status)
		{
			case IsOpen.Close:
				return "Close";
			case IsOpen.Open:
				return "Open";
		}

		return "";
	}

	public enum IsOpen
	{
		Close,
		Open
	};
}
