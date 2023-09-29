namespace DeliveryService.Contracts.Order;

public record CheckoutPaymentRequest(
	 string OrderId,
	 string CardNumber,
	 string CVV,
	 string ExpiryMonthYear,
	 double OrderTotalSum,
	 int CartTotalItems);

