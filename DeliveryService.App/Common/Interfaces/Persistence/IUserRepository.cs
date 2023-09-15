using DeliveryService.Domain.User;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IUserRepository : IGenericRepository<UserEntity>
{
	Task<UserEntity?> FindUserByEmail(string email);

	Task<UserEntity?> FindUserByRegisteredEmail(string email);

	Task<UserEntity?> FindUserByRegisteredEmailWithRole(string email);
}
