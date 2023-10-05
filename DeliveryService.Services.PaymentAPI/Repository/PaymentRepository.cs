using DeliveryService.Services.PaymentAPI.DbContexts;
using DeliveryService.Services.PaymentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.PaymentAPI.Repository;

public class PaymentRepository : IPaymentRepository
{
	private readonly DbContextOptions<ApplicationDbContext> _dbContext;

	public PaymentRepository(DbContextOptions<ApplicationDbContext> dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task<bool> AddPayment(PaymentEntity payment)
	{
		await using var _db = new ApplicationDbContext(_dbContext);
		_db.Payments.Add(payment);
		if (await _db.SaveChangesAsync() > 0)
			return true;

		return false;
	}

	public async Task<PaymentEntity> FindPaymentByOrderId(string orderId)
	{
		await using var _db = new ApplicationDbContext(_dbContext);
		var payment = _db.Payments.FirstOrDefault(payment => payment.OrderId == orderId);
		
		return payment;
	}
}
