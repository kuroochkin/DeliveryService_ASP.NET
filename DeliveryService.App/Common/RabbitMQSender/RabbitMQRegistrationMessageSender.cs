using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace DeliveryService.App.Common.RabbitMQSender;

public class RabbitMQRegistrationMessageSender : IRabbitMQRegistrationMessageSender
{
	private readonly string _hostname;
	private readonly string _password;
	private readonly string _username;
	private IConnection _connection;

	public RabbitMQRegistrationMessageSender()
	{
		_hostname = "dockercompose10352310043466506766-rabbitmq-1";
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
			var headers = new Dictionary<string, object>
			{
				{"Operation", "Registration"},
            };
			var properties = channel.CreateBasicProperties();
			properties.Headers = headers;
			channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: properties, body: body);
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
