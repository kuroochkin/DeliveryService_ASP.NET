using DeliveryService.Domain.User;

namespace DeliveryService.App.Common.Interfaces.Auth;

public interface IJwtTokenGenerator
{
	string GenerateToken(UserEntity user);
}
