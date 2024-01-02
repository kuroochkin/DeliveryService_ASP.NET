using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;
using DeliveryService.Services.RestaurantAPI.App.Common.Messages;
using DeliveryService.Services.RestaurantAPI.Domain.Manager;

namespace DeliveryService.Services.RestaurantAPI.App.Common.Messaging;

public class RabbitMQCreateManagerConsumer : BackgroundService
{

    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQCreateManagerConsumer(IServiceProvider serviceProvider)
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
        _channel.QueueDeclare(queue: "RestaurantAPI: CreateManagerQueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var checkoutHeaderDto = JsonConvert.DeserializeObject<CreateManagerDTO>(content);
            await HandleMessage(checkoutHeaderDto);

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("RestaurantAPI: CreateManagerQueue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleMessage(CreateManagerDTO createManagerDto)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var _unitOfWork =
                scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var managerId = Guid.Parse(createManagerDto.UserId);
            var manager = new ManagerEntity(managerId, createManagerDto.LastName, createManagerDto.FirstName);

            await _unitOfWork.Managers.Add(manager);
            await _unitOfWork.CompleteAsync();
        }
    }
}
