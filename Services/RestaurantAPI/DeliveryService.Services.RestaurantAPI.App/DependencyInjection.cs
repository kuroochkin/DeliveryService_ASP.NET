using DeliveryService.Services.RestaurantAPI.App.Common.Behavior;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;


namespace DeliveryService.Services.RestaurantAPI.App;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddMediatR(typeof(DependencyInjection).Assembly);
		services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		return services;
	}
}
