using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Role;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class RoleRepository : GenericRepository<RoleEntity>, IRoleRepository
{
	public RoleRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<RoleEntity?> FindRoleByName(string name)
	{
		return await _context.Roles
				.FirstOrDefaultAsync(role => role.Name == name);
	}
}
