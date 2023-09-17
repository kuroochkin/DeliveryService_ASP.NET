using DeliveryService.Domain.Customer;
using DeliveryService.Domain.PaymentOrder;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IPaymentOrderRepository : IGenericRepository<PaymentOrderEntity>
{

}
