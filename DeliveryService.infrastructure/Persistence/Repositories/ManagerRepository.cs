using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Manager;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class ManagerRepository : GenericRepository<ManagerEntity>, IManagerRepository
{
	public ManagerRepository(ApplicationDbContext context) : base(context)
	{
	}
}
