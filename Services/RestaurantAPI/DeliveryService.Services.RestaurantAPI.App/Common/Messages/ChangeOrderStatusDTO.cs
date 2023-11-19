using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.RestaurantAPI.App.Common.Messages;

public class ChangeOrderStatusDTO : BaseMessage
{
	public string OrderId { get; set; }
}

public class ChangeOrderStatusAndStartManagerDTO : BaseMessage
{
	public string OrderId { get; set; }
	public string ManagerId { get; set; }
}
