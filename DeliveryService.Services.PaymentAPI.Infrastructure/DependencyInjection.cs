using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.infrastructure.Auth;
using DeliveryService.Services.PaymentAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.PaymentAPI.Infrastructure.Persistence;
using DeliveryService.Services.PaymentAPI.Infrastructure.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DeliveryService.Services.PaymentAPI.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		services.AddScoped<IPaymentRepository, PaymentRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		//services.AddAuth(configuration);

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
