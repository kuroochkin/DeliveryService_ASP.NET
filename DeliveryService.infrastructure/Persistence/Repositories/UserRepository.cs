using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.User;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
	public UserRepository(ApplicationDbContext context) : base(context)
	{
	}
}
