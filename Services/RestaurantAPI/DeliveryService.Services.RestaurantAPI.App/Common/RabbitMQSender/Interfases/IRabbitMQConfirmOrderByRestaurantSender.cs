﻿using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.RestaurantAPI.App.Common.RabbitMQSender.Interfases;

public interface IRabbitMQConfirmOrderByRestaurantSender
{
	void SendMessage(BaseMessage baseMessage, string queueName);
}
