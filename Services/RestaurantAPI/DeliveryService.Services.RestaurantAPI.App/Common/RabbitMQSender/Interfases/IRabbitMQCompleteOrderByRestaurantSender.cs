using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.RestaurantAPI.App.Common.RabbitMQSender.Interfases;

public interface IRabbitMQCompleteOrderByRestaurantSender
{
	void SendMessage(BaseMessage baseMessage, string queueName);
}
