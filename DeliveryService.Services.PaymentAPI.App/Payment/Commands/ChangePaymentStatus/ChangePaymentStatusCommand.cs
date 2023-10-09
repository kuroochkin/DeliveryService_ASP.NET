using ErrorOr;
using MediatR;

namespace DeliveryService.Services.PaymentAPI.App.Payment.Commands.ChangePayment;

public record ChangePaymentStatusCommand(
    string OrderId) : IRequest<ErrorOr<bool>>;

