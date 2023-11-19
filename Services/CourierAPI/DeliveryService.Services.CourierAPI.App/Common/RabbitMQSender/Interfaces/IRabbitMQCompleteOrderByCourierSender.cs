using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.CourierAPI.App.Common.RabbitMQSender.Interfaces;

public interface IRabbitMQCompleteOrderByCourierSender
{
	void SendMessage(BaseMessage baseMessage, string queueName);
}
