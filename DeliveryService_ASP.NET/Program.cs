using DeliveryService.infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
	builder.Services
		.AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{

	app.Run();
}
