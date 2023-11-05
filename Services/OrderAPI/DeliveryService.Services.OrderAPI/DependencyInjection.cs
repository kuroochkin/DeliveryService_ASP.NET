using DeliveryService.Services.OrderAPI.Mapping;
using Microsoft.OpenApi.Models;

namespace DeliveryService.Services.OrderAPI;

public static class DependencyInjection
{
	public static IServiceCollection AddPresentation(this IServiceCollection services)
	{
		services.AddControllers();
		services.AddEndpointsApiExplorer();

		services.AddSwaggerGen(option =>
		{
			//option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
			//{
			//	Name = "Authorization",
			//	Type = SecuritySchemeType.ApiKey,
			//	Scheme = "Bearer",
			//	BearerFormat = "JWT",
			//	In = ParameterLocation.Header,
			//	Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
			//});
			//option.AddSecurityRequirement(new OpenApiSecurityRequirement
			//{
			//	{
			//		new OpenApiSecurityScheme
			//		{
			//			Reference = new OpenApiReference
			//			{
			//				Type = ReferenceType.SecurityScheme,
			//				Id = "Bearer"
			//			}
			//		},
			//		new string[] {}
			//	}
			//});
			option.SwaggerDoc("v1", new OpenApiInfo { Title = "DeliveryService.Services.OrderAPI", Version = "v1" });
		});

		services.AddMappings();
		return services;
	}
}
