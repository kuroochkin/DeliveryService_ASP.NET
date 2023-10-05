using DeliveryService.Services.PaymentAPI.Models;

namespace DeliveryService.Services.PaymentAPI.Repository;

public interface IPaymentRepository
{
	Task<bool> AddPayment(PaymentEntity payment);
	Task<PaymentEntity> FindPaymentByOrderId(string orderId);
}
