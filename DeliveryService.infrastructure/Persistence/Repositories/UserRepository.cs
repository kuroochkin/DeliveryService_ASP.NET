using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class UserRepository : GenericRepository<UserEntity>, IUserRepository
{
	public UserRepository(ApplicationDbContext context) : base(context)
	{
		
	}

	public async Task<UserEntity?> FindUserByEmail(string email)
	{
		return await _context.Users
			.FirstOrDefaultAsync(x => x.Email == email);
	}

	public async Task<UserEntity?> FindUserByRegisteredEmail(string email)
	{
		return await _context.Users
			.FirstOrDefaultAsync(x => x.Email == email);
	}
}
