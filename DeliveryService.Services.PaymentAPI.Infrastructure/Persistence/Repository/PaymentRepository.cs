using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.PaymentAPI.Domain.Payment;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.PaymentAPI.Infrastructure.Persistence.Repository;

public class PaymentRepository : GenericRepository<PaymentEntity>, IPaymentRepository
{
	public PaymentRepository(ApplicationDbContext context) : base(context)
	{
	}

	//public async Task<bool> AddPayment(PaymentEntity payment)
	//{
	//	await using var _db = new ApplicationDbContext(_dbContext);
	//	_db.Payments.Add(payment);
	//	if (await _db.SaveChangesAsync() > 0)
	//		return true;

	//	return false;
	//}

	public async Task<PaymentEntity?> FindPaymentByOrderId(Guid orderId)
	{
		return await _context.Payments
			.FirstOrDefaultAsync(payment => payment.OrderId == orderId);
	}
}
