using DeliveryService.App.Common.RabbitMQSender;

namespace DeliveryService.Services.PaymentAPI.Messages;

public class ChangeOrderPaymentStatusDTO : BaseMessage
{
	public string OrderId {  get; set; }
	public bool PaymentStatus {  get; set; }
}
