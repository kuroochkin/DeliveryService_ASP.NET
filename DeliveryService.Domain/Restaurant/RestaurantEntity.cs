using static DeliveryService.Domain.Order.OrderEntity;

namespace DeliveryService.Domain.Restaraunt;

public class RestarauntEntity
{
	public Guid Id { get; set; }

	public string Name { get; set; }

	public string? Address { get; set; }

	public IsOpen Status { get; set; }

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
