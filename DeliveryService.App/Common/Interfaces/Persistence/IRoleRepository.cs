using DeliveryService.Domain.Product;
using DeliveryService.Domain.Role;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IRoleRepository : IGenericRepository<RoleEntity>
{
	Task<RoleEntity?> FindRoleByName(string name);
}
