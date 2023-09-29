using DeliveryService.Services.PaymentAPI.Messages;
using DeliveryService.Services.PaymentAPI.Models;
using DeliveryService.Services.PaymentAPI.Repository;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DeliveryService.Services.PaymentAPI.Messaging;

public class RabbitMQCheckoutConsumer : BackgroundService
{
	private readonly IPaymentRepository _paymentRepository;
	private IConnection _connection;
	private IModel _channel;

	public RabbitMQCheckoutConsumer(IPaymentRepository orderRepository)
	{
		_paymentRepository = orderRepository;
		var factory = new ConnectionFactory
		{
			HostName = "localhost",
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
		PaymentEntity payment = new()
		{
			OrderId = paymentDto.OrderId,
			UserId = paymentDto.UserId,
			FirstName = paymentDto.FirstName,
			LastName = paymentDto.LastName,
			Email = paymentDto.Email,
			CardNumber = paymentDto.CardNumber,
			CVV = paymentDto.CVV,
			ExpiryMonthYear = paymentDto.ExpiryMonthYear,
			OrderTotalSum = paymentDto.OrderTotalSum,
			CartTotalItems = paymentDto.CartTotalItems,
		};

		await _paymentRepository.AddPayment(payment);
	}
}
