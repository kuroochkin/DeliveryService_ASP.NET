namespace DeliveryService.Services.PaymentAPI.Contracts.Payment.Requests;

public record ChangePaymentStatusRequest(
	string OrderId);

