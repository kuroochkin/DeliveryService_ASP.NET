namespace DeliveryService.App.Common.RabbitMQSender;

public interface IRabbitMQOrderMessageSender
{
	void SendMessage(BaseMessage baseMessage, string queueName);
}
