using System.Text;
using DeliveryService.Services.CourierAPI.App.Common.Messages;
using DeliveryService.Services.CourierAPI.App.Common.Interfaces.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using DeliveryService.Services.CourierAPI.Domain.Courier;

namespace DeliveryService.Services.CourierAPI.App.Common.Messaging;

public class RabbitMQCreateCourierConsumer : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private IConnection _connection;
    private IModel _channel;

    public RabbitMQCreateCourierConsumer(IServiceProvider serviceProvider)
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
        _channel.QueueDeclare(queue: "CourierAPI: CreateCourierQueue", false, false, false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
            var checkoutHeaderDto = JsonConvert.DeserializeObject<CreateCourierDTO>(content);
            await HandleMessage(checkoutHeaderDto);

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume("CourierAPI: CreateCourierQueue", false, consumer);

        return Task.CompletedTask;
    }

    private async Task HandleMessage(CreateCourierDTO createCourierDto)
    {
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var _unitOfWork =
                scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var courierId = Guid.Parse(createCourierDto.UserId);
            var courier = new CourierEntity(courierId, createCourierDto.LastName, createCourierDto.FirstName);

            await _unitOfWork.Couriers.Add(courier);
            await _unitOfWork.CompleteAsync();
        }
    }
}
