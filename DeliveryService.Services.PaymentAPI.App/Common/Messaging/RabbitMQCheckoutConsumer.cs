using DeliveryService.Services.PaymentAPI.App.Common.Messages;
using DeliveryService.Services.PaymentAPI.Domain.Payment;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DeliveryService.Services.PaymentAPI.App.Common.Messaging;

public class RabbitMQCheckoutConsumer : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;
	private IConnection _connection;
	private IModel _channel;

	public RabbitMQCheckoutConsumer(IServiceProvider serviceProvider)
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
		_channel.QueueDeclare(queue: "checkoutqueue", false, false, false, arguments: null);
	}

	protected override Task ExecuteAsync(CancellationToken stoppingToken)
	{
		stoppingToken.ThrowIfCancellationRequested();

		var consumer = new EventingBasicConsumer(_channel);
		consumer.Received += async (ch, ea) =>
		{
			var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			var checkoutHeaderDto = JsonConvert.DeserializeObject<PaymentDTO>(content);
			await HandleMessage(checkoutHeaderDto);

			_channel.BasicAck(ea.DeliveryTag, false);
		};

		_channel.BasicConsume("checkoutqueue", false, consumer);

		return Task.CompletedTask;
	}

	private async Task HandleMessage(PaymentDTO paymentDto)
	{

		using (IServiceScope scope = _serviceProvider.CreateScope())
		{
			var _unitOfWork =
				scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

			PaymentEntity payment = new()
			{
				Id = Guid.NewGuid(),
				OrderId = paymentDto.OrderId,
				UserId = paymentDto.UserId,
				FirstName = paymentDto.FirstName,
				LastName = paymentDto.LastName,
				Email = paymentDto.Email,
				CardNumber = paymentDto.CardNumber,
				CVV = paymentDto.Cvv,
				ExpiryMonthYear = paymentDto.ExpiryMonthYear,
				OrderTotalSum = paymentDto.OrderTotalSum,
				CartTotalItems = paymentDto.CartTotalItems,
			};

			if (await _unitOfWork.Payments.Add(payment))
			{
				await _unitOfWork.CompleteAsync();
			}
		}
	}
}