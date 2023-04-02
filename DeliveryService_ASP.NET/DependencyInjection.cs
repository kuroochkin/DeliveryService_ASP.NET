using DeliveryService.API.Common.Mapping;

namespace DeliveryService.API;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddSwaggerGen();
		services.AddMappings();
		services.AddEndpointsApiExplorer();

		return services;
	}
}