namespace DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;

public interface IUnitOfWork
{
	IPaymentRepository Payments { get; }

	Task<bool> CompleteAsync();
}
