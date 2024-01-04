using DeliveryService.Services.CourierAPI.App;
using DeliveryService.Services.CourierAPI.Infrastructure;
using DeliveryService.Services.CourierAPI;
using DeliveryService.Services.CourierAPI.App.Common.RabbitMQSender.Interfaces;
using DeliveryService.Services.CourierAPI.App.Common.RabbitMQSender;
using DeliveryService.Services.CourierAPI.App.Common.Messaging;

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

builder.Services.AddSingleton<IRabbitMQConfirmOrderByCourierSender, RabbitMQConfirmOrderByCourierSender>();
builder.Services.AddSingleton<IRabbitMQCompleteOrderByCourierSender, RabbitMQCompleteOrderByCourierSender>();
builder.Services.AddHostedService<RabbitMQCreateCourierConsumer>();

var app = builder.Build();
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}

	app.UseCors("AllowAllHeaders");

	app.UseHttpsRedirection();

	app.UseAuthentication();
	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
