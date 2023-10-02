using AutoMapper;
using DeliveryService.Services.PaymentAPI.DbContexts;
using DeliveryService.Services.PaymentAPI.Mapping;
using DeliveryService.Services.PaymentAPI.Messaging;
using DeliveryService.Services.PaymentAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();

services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeliveryService.Services.PaymentAPI", Version = "v1" });
});

services.AddDbContext<ApplicationDbContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("NpgServer"));
});

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
services.AddSingleton(mapper);
services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var optionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
optionBuilder.UseNpgsql(builder.Configuration.GetConnectionString("NpgServer"));
services.AddSingleton<IPaymentRepository>(new PaymentRepository(optionBuilder.Options));

services.AddHostedService<RabbitMQCheckoutConsumer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
