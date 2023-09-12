using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Role;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class RoleRepository : GenericRepository<RoleEntity>, IRoleRepository
{
	public RoleRepository(ApplicationDbContext context) : base(context)
	{
	}
}
