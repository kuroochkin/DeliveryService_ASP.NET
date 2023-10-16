using DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.PaymentAPI.Infrastructure.Persistence.Repository;

public class GenericRepository<T> : IGenericRepository<T>
	where T : class
{
	protected readonly ApplicationDbContext _context;

	public GenericRepository(ApplicationDbContext context)
	{
		_context = context;
	}

	public async Task<bool> Add(T entity)
	{
		await _context.Set<T>().AddAsync(entity);
		return true;
	}

	public bool Delete(T entity)
	{
		_context.Set<T>().Remove(entity);
		return true;
	}

	public async Task<T?> FindById(Guid id)
	{
		return await _context.Set<T>().FindAsync(id);
	}

	public async Task<IEnumerable<T>> GetAll()
	{
		return await _context.Set<T>().ToListAsync();
	}

	public bool Update(T entity)
	{
		_context.Set<T>().Update(entity);
		return true;
	}
}
