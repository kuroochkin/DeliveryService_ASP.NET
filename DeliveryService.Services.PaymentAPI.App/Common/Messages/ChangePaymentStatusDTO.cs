using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.PaymentAPI.App.Common.Messages;

public class ChangePaymentStatusDTO : BaseMessage
{
	public string OrderId { get; set; }
	public bool PaymentStatus { get; set; }
}

