using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.infrastructure.Persistence;
using DeliveryService.infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeliveryService.infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
	{
		services.AddScoped<ICourierRepository, CourierRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();

		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
		});

		return services;
	}
}