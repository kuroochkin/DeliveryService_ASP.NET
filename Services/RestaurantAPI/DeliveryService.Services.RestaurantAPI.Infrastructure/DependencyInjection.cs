using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.infrastructure.Auth;
using DeliveryService.Services.RestaurantAPI.App.Common.Interfaces;
using DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace DeliveryService.Services.RestaurantAPI.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		services.AddScoped<IRestaurantRepository, RestaurantRepository>();
		services.AddScoped<IManagerRepository, ManagerRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		//!!!!!!!!!!!!!!!!!!!!!!!!!
		//services.AddAuth(configuration);
		//!!!!!!!!!!!!!!!!!!!!!!!!!

		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseNpgsql(configuration.GetConnectionString("NpgServer"));
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
