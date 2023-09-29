using ErrorOr;
using MediatR;

namespace DeliveryService.App.Order.Commands.CheckoutPayment;

public record CheckoutPaymentCommand(
	 string OrderId,
	 string CustomerId,
	 string CardNumber,
	 string CVV,
	 string ExpiryMonthYear,
	 double OrderTotalSum,
	 int CartTotalItems) : IRequest<ErrorOr<bool>>;



