using DeliveryService.App.Auth.Commands;
using DeliveryService.App.Auth.Common;
using DeliveryService.Contracts.Auth;
using Mapster;

namespace DeliveryService.API.Common.Mapping;

public class AuthMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<AuthenticationResult, AuthenticationResponse>();
		config.NewConfig<RegisterRequest, RegisterCommand>();
	}
}
