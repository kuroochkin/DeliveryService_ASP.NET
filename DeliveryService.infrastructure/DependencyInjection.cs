using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.infrastructure.Auth;
using DeliveryService.infrastructure.Persistence;
using DeliveryService.infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DeliveryService.infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services, 
		ConfigurationManager configuration)
	{
		services.AddScoped<ICourierRepository, CourierRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IOrderItemRepository, OrderItemRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		

		services.AddAuth(configuration);

		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
		});


		return services;
	}

	public static IServiceCollection AddAuth(this IServiceCollection services,
		ConfigurationManager configuration)
	{
		var jwtSettings = new JwtSettings();
		configuration.Bind(JwtSettings.SectionName, jwtSettings);
		services.AddSingleton(Options.Create(jwtSettings));

		
		services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

		services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = jwtSettings.Issuer,
				ValidAudience = jwtSettings.Audience,
				IssuerSigningKey = new SymmetricSecurityKey(
					Encoding.UTF8.GetBytes(jwtSettings.Secret))
			});

		return services;
	}
}