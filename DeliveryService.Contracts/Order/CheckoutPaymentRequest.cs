namespace DeliveryService.Contracts.Order;

public record CheckoutPaymentRequest(
	 string OrderId,
	 string CardNumber,
	 string Cvv,
	 string ExpiryMonthYear,
	 double OrderTotalSum,
	 int CartTotalItems);

