namespace DeliveryService.App.Common.RabbitMQSender;

public class BaseMessage
{
	public int Id { get; set; }
	public DateTime MessageCreated { get; set; }
}
