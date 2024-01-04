using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using DeliveryService.Services.OrderAPI.App.Common.Messages;
using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;
using DeliveryService.Services.OrderAPI.Domain.Customer;

namespace DeliveryService.Services.OrderAPI.App.Common.Messaging;

public class RabbitMQCreateCustomerConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQCreateCustomerConsumer(IServiceProvider serviceProvider)
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
        _channel.QueueDeclare(queue: "OrderAPI: CreateCustomerQueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var checkoutHeaderDto = JsonConvert.DeserializeObject<CreateCustomerDTO>(content);
            await HandleMessage(checkoutHeaderDto);

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("OrderAPI: CreateCustomerQueue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleMessage(CreateCustomerDTO createCustomerDto)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var _unitOfWork =
                scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var customerId = Guid.Parse(createCustomerDto.UserId);
            var customer = new CustomerEntity(customerId, createCustomerDto.LastName, createCustomerDto.FirstName);
            
            await _unitOfWork.Customers.Add(customer);
            await _unitOfWork.CompleteAsync();
        }
    }
}
