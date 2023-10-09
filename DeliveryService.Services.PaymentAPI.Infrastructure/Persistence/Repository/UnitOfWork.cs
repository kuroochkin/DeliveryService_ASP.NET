using DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;


namespace DeliveryService.Services.PaymentAPI.Infrastructure.Persistence.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
	private readonly ApplicationDbContext _context;

	public IPaymentRepository Payments { get; }

	public UnitOfWork(ApplicationDbContext context,
		IPaymentRepository payments)
	{
		_context = context;
		Payments = payments;
	}

	public async Task<bool> CompleteAsync()
	{
		return await _context.SaveChangesAsync() > 0;
	}

	public void Dispose()
	{
		_context?.Dispose();
	}
}
