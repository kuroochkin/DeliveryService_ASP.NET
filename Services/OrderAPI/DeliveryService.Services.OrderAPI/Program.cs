using DeliveryService.Services.OrderAPI.Infrastructure;
using DeliveryService.Services.OrderAPI.App;
using DeliveryService.Services.OrderAPI;

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

//builder.Services.AddSingleton<IRabbitMQMessageSender, RabbitMQMessageSender>();
//builder.Services.AddHostedService<RabbitMQCheckoutConsumer>();

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

