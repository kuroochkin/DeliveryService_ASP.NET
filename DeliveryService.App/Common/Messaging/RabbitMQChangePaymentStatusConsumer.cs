using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Common.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DeliveryService.Services.PaymentAPI.Messaging;

public class RabbitMQChangePaymentStatusConsumer : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private IConnection _connection;
	private IModel _channel;

	public RabbitMQChangePaymentStatusConsumer(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider;

		var factory = new ConnectionFactory
		{
			HostName = "dockercompose10352310043466506766-rabbitmq-1",
			UserName = "rmuser",
			Password = "rmpassword"
		};

		_connection = factory.CreateConnection();
		_channel = _connection.CreateModel();
		_channel.QueueDeclare(queue: "changeStatusqueue", false, false, false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += async (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			var checkoutHeaderDto = JsonConvert.DeserializeObject<ChangeOrderPaymentStatusDTO>(content);
			await HandleMessage(checkoutHeaderDto);

			_channel.BasicAck(ea.DeliveryTag, false);
		};

		_channel.BasicConsume("changeStatusqueue", false, consumer);

		return Task.CompletedTask;
	}

	private async Task HandleMessage(ChangeOrderPaymentStatusDTO paymentDto)
	{
		using (IServiceScope scope = _serviceProvider.CreateScope())
		{
			var _unitOfWork =
				scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			Guid.TryParse(paymentDto.OrderId, out var orderId);

			var order = await _unitOfWork.Orders.FindOrderWithCustomerAndCourierAndManager(orderId);

			order.PaymentStatus = paymentDto.PaymentStatus;

			await _unitOfWork.CompleteAsync();
		}
	}
}
