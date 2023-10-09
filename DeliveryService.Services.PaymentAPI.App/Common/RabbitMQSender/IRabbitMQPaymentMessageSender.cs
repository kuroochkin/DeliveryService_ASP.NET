using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.PaymentAPI.App.Common.RabbitMQSender;

public interface IRabbitMQPaymentMessageSender
{
	void SendMessage(BaseMessage baseMessage, string queueName);
}
