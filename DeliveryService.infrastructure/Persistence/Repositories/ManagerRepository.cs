using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Manager;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class ManagerRepository : GenericRepository<ManagerEntity>, IManagerRepository
{
	public ManagerRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<ManagerEntity?> FindManagerWithRestaurantById(Guid id)
	{
		return await _context.Managers
			.Include(manager => manager.Restaurant)
			.FirstOrDefaultAsync(manager => manager.Id == id);
	}
}
