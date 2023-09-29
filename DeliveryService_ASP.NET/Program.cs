using DeliveryService.API;
using DeliveryService.App;
using DeliveryService.App.Common.RabbitMQSender;
using DeliveryService.infrastructure;

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

builder.Services.AddSingleton<IRabbitMQOrderMessageSender, RabbitMQOrderMessageSender>();

var app = builder.Build();
{
	if (app.Environment.IsDevelopment())
	{
		app.UseSwagger();
		app.UseSwaggerUI();
	}


	app.UseCors("AllowAllHeaders");

	//app.UseHttpsRedirection();

	app.UseAuthentication();
	app.UseAuthorization();

	app.MapControllers();

	app.Run();
}
