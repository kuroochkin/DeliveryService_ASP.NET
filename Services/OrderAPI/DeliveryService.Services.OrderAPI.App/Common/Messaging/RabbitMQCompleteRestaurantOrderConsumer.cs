﻿using DeliveryService.Services.OrderAPI.App.Common.Messages;
using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using static DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity;

namespace DeliveryService.Services.OrderAPI.App.Common.Messaging;

public class RabbitMQCompleteRestaurantOrderConsumer : BackgroundService
{ 
	private readonly IServiceProvider _serviceProvider;
	private IConnection _connection;
	private IModel _channel;

	public RabbitMQCompleteRestaurantOrderConsumer(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;

		var factory = new ConnectionFactory
		{
			HostName = "rabbitmq",
			UserName = "rmuser",
			Password = "rmpassword"
		};

		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(queue: "RestaurantAPI: CompleteOrderByRestaurant", false, false, false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += async (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			var checkoutHeaderDto = JsonConvert.DeserializeObject<CompleteOrderByRestaurantDTO>(content);
			await HandleMessage(checkoutHeaderDto);

			_channel.BasicAck(ea.DeliveryTag, false);
		};

		_channel.BasicConsume("RestaurantAPI: CompleteOrderByRestaurant", false, consumer);

		return Task.CompletedTask;
	}

	private async Task HandleMessage(CompleteOrderByRestaurantDTO completeDto)
	{

		using (IServiceScope scope = _serviceProvider.CreateScope())
		{
			var _unitOfWork =
				scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			var orderId = Guid.Parse(completeDto.OrderId);

			//Находим нужный заказ
			var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourierAndManager(orderId);

			//Меняем его статус
			order.Status = OrderStatus.EndRestaurant;
			order.EndRestaurant = DateTime.UtcNow;

			await _unitOfWork.CompleteAsync();
		}
	}
}
