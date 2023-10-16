namespace DeliveryService.App.Common.RabbitMQSender;

public interface IRabbitMQMessageSender
{
	void SendMessage(BaseMessage baseMessage, string queueName);
}
