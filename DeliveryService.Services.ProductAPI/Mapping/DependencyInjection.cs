﻿using Mapster;
using MapsterMapper;
using System.Reflection;

namespace DeliveryService.Services.ProductAPI.Mapping;

public static class DependencyInjection
{
	public static IServiceCollection AddMappings(this IServiceCollection services)
	{
		var config = TypeAdapterConfig.GlobalSettings;
		config.Scan(Assembly.GetExecutingAssembly());

		services.AddSingleton(config);
		services.AddScoped<IMapper, ServiceMapper>();

		return services;
	}
}