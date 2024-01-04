using DeliveryService.App.Common.RabbitMQSender;
using System.Text;
using DeliveryService.AuthAPI.RabbitMQ.Senders.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DeliveryService.AuthAPI.RabbitMQ.Senders;

public class RabbitMQCreateUserWithRoleSender : IRabbitMQCreateUserWithRoleSender
{
    private readonly string _hostname;
    private readonly string _password;
    private readonly string _username;
    private IConnection _connection;

    public RabbitMQCreateUserWithRoleSender()
    {
        _hostname = "rabbitmq";
        _password = "rmpassword";
        _username = "rmuser";
    }

    public void SendMessage(BaseMessage message, string queueName)
    {
        if (ConnectionExists())
        {
            using var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: queueName, false, false, false, arguments: null);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }

    private void CreateConnection()
    {
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                UserName = _username,
                Password = _password
            };
            _connection = factory.CreateConnection();
        }
        catch (Exception)
        {
            //log exception
        }
    }

    private bool ConnectionExists()
    {
        if (_connection != null)
        {
            return true;
        }
        CreateConnection();
        return _connection != null;
    }
}
