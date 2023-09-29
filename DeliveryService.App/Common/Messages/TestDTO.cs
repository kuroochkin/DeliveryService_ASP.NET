using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.App.Common.Messages;

public class TestDTO : BaseMessage
{
	public string Name { get; set; }
}
