using DeliveryService.Services.PaymentAPI.Domain.Payment;

namespace DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;

public interface IPaymentRepository : IGenericRepository<PaymentEntity>
{
	//Task<bool> AddPayment(PaymentEntity payment);
	Task<PaymentEntity?> FindPaymentByOrderId(Guid orderId);
}
