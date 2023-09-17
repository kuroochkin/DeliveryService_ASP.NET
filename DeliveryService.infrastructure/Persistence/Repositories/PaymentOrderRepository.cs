using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.PaymentOrder;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class PaymentOrderRepository : GenericRepository<PaymentOrderEntity>, IPaymentOrderRepository
{
	public PaymentOrderRepository(ApplicationDbContext context) : base(context)
	{
	}
}
