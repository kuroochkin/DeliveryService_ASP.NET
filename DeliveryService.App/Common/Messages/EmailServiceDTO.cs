using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.App.Common.Messages;

public class EmailServiceDTO : BaseMessage
{
	public string? Email { get; set; }
	public string? Operation { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
}
