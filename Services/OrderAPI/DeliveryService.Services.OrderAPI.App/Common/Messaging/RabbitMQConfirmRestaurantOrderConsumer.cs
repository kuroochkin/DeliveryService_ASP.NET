using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using Microsoft.Extensions.Hosting;
using System.Text;
using Newtonsoft.Json;
using DeliveryService.Services.OrderAPI.App.Common.Messages;
using static DeliveryService.Services.OrderAPI.Domain.Order.OrderEntity;

namespace DeliveryService.Services.OrderAPI.App.Common.Messaging;

//Прослушивание очереди на прием заказа рестораном от заказчика
public class RabbitMQConfirmRestaurantOrderConsumer : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private IConnection _connection;
	private IModel _channel;

	public RabbitMQConfirmRestaurantOrderConsumer(IServiceProvider serviceProvider)
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
		_channel.QueueDeclare(queue: "RestaurantAPI: ConfirmOrderByRestaurant", false, false, false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += async (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			var checkoutHeaderDto = JsonConvert.DeserializeObject<ConfirmOrderByRestaurantDTO>(content);
			await HandleMessage(checkoutHeaderDto);

			_channel.BasicAck(ea.DeliveryTag, false);
		};

		_channel.BasicConsume("RestaurantAPI: ConfirmOrderByRestaurant", false, consumer);

		return Task.CompletedTask;
	}

	private async Task HandleMessage(ConfirmOrderByRestaurantDTO confirmDto)
	{

		using (IServiceScope scope = _serviceProvider.CreateScope())
		{
			var _unitOfWork =
				scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			var orderId = Guid.Parse(confirmDto.OrderId);

			var managerId = Guid.Parse(confirmDto.ManagerId);

			//Находим нужный заказ
			var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourierAndManager(orderId);

			//Прикрепляем менеджера к заказу
			order.ManagerId = managerId;

			//Меняем его статус
			order.Status = OrderStatus.ConfirmedRestaurant;
			order.ConfirmedRestaurant = DateTime.UtcNow;

		    await _unitOfWork.CompleteAsync();
		}
	}
}
