using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.OrderAPI.App.Common.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using static DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity;

namespace DeliveryService.Services.OrderAPI.App.Common.Messaging;

public class RabbitMQConfirmCourierOrderConsumer : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private IConnection _connection;
	private IModel _channel;

	public RabbitMQConfirmCourierOrderConsumer(IServiceProvider serviceProvider)
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
		_channel.QueueDeclare(queue: "CourierAPI: ConfirmOrderByCourier", false, false, false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += async (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			var checkoutHeaderDto = JsonConvert.DeserializeObject<ConfirmOrderByCourierDTO>(content);
			await HandleMessage(checkoutHeaderDto);

			_channel.BasicAck(ea.DeliveryTag, false);
		};

		_channel.BasicConsume("CourierAPI: ConfirmOrderByCourier", false, consumer);

		return Task.CompletedTask;
	}

	private async Task HandleMessage(ConfirmOrderByCourierDTO confirmDto)
	{

		using (IServiceScope scope = _serviceProvider.CreateScope())
		{
			var _unitOfWork =
				scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			var orderId = Guid.Parse(confirmDto.OrderId);

			var courierId = Guid.Parse(confirmDto.CourierId);

			//Находим нужный заказ
			var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourierAndManager(orderId);

			//Прикрепляем менеджера к заказу
			order.CourierId = courierId;

			//Меняем его статус
			order.Status = OrderStatus.ConfirmedCourier;
			order.ConfirmedCourier = DateTime.UtcNow;

			await _unitOfWork.CompleteAsync();
		}
	}
}
