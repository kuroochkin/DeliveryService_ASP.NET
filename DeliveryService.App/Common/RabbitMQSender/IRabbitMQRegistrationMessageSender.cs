namespace DeliveryService.App.Common.RabbitMQSender;

public interface IRabbitMQRegistrationMessageSender
{
	void SendMessage(BaseMessage baseMessage, string queueName);
}
