namespace DeliveryService.Services.OrderAPI.App.Common.Messages;

public class ConfirmOrderByRestaurantDTO
{
	public string OrderId { get; set; }
	public string ManagerId { get; set; }
}

public class CompleteOrderByRestaurantDTO
{
	public string OrderId { get; set; }
}

public class ConfirmOrderByCourierDTO
{
	public string OrderId { get; set; }
	public string CourierId { get; set; }
}

public class CompleteOrderByCourierDTO
{
	public string OrderId { get; set; }
}