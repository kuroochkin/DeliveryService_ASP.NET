using DeliveryService.Services.PaymentAPI.Infrastructure;
using DeliveryService.Services.PaymentAPI;
using DeliveryService.Services.PaymentAPI.App;
using DeliveryService.Services.PaymentAPI.App.Common.Messaging;
using DeliveryService.Services.PaymentAPI.App.Common.RabbitMQSender;

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

builder.Services.AddSingleton<IRabbitMQPaymentMessageSender, RabbitMQPaymentMessageSender>();
builder.Services.AddHostedService<RabbitMQPaymentCheckoutConsumer>();

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
