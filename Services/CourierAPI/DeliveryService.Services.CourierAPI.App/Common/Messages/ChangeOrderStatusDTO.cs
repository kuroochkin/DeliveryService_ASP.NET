using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.CourierAPI.App.Common.Messages;

public class ChangeOrderStatusDTO : BaseMessage
{
	public string OrderId { get; set; }
}

public class ChangeOrderStatusAndStartCourierDTO : BaseMessage
{
	public string OrderId { get; set; }
	public string CourierId { get; set; }
}

