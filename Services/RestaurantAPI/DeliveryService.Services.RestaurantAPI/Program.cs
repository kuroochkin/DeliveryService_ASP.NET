using DeliveryService.Services.RestaurantAPI;
using DeliveryService.Services.RestaurantAPI.App;
using DeliveryService.Services.RestaurantAPI.App.Common.Messaging;
using DeliveryService.Services.RestaurantAPI.App.Common.RabbitMQSender;
using DeliveryService.Services.RestaurantAPI.App.Common.RabbitMQSender.Interfases;
using DeliveryService.Services.RestaurantAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services
		.AddApplication()
		.AddInfrastructure(builder.Configuration)
		.AddPresentation();

	builder.Services
		.AddCors(options =>
		{
			options.AddPolicy("AllowAllHeaders", builder =>
			{
				builder.AllowAnyOrigin()
					   .AllowAnyHeader()
					   .AllowAnyMethod();
			});
		});
}

builder.Services.AddSingleton<IRabbitMQConfirmOrderByRestaurantSender, RabbitMQConfirmOrderByRestaurantSender>();
builder.Services.AddSingleton<IRabbitMQCompleteOrderByRestaurantSender, RabbitMQCompleteOrderByRestaurantSender>();
builder.Services.AddHostedService<RabbitMQCreateManagerConsumer>();

var app = builder.Build();
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}


	app.UseCors("AllowAllHeaders");

	//app.UseHttpsRedirection();

	//app.UseAuthentication();
	//app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
