using Amazon.S3;
using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.App.Common.Interfaces.Minio;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.infrastructure.Auth;
using DeliveryService.infrastructure.Minio;
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
		services.AddScoped<IUserRepository, UserRepository>();
		services.AddScoped<IRoleRepository, RoleRepository>();
		services.AddScoped<ICourierRepository, CourierRepository>();
		services.AddScoped<ICustomerRepository, CustomerRepository>();
		services.AddScoped<IManagerRepository, ManagerRepository>();
		services.AddScoped<IRestaurantRepository, RestaurantRepository>();
		services.AddScoped<IOrderRepository, OrderRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IOrderItemRepository, OrderItemRepository>();
		services.AddScoped<ISectionRepository, SectionRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		

		services.AddAuth(configuration);

		services.AddMinio(configuration);

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

	public static IServiceCollection AddMinio(this IServiceCollection services,
		ConfigurationManager configuration)
	{
		var minioSettings = new MinioSettings();
		configuration.Bind(MinioSettings.SectionName, minioSettings);
		services.AddSingleton(Options.Create(minioSettings));

		services.AddSingleton(new AmazonS3Client(
			minioSettings.AccessKey,
			minioSettings.SecretKey,
			new AmazonS3Config
			{
				ServiceURL = minioSettings.ServiceUrl,
			})
		);
		services.AddScoped<IStorageService, StorageService>();

		return services;
	}
}