namespace DeliveryService.App.Common.Messages;

public class ChangeOrderPaymentStatusDTO
{
	public string OrderId { get; set; }
	public bool PaymentStatus { get; set; }
}
