using DeliveryService.AuthAPI.Data;
using DeliveryService.AuthAPI.Model;
using DeliveryService.AuthAPI.Services;
using DeliveryService.AuthAPI.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var services = builder.Services;
var configuration = builder.Configuration;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddDbContext<ApplicationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddDefaultTokenProviders();

var identitySettings = new IdentityServerSettings();
configuration.Bind(IdentityServerSettings.SectionName, identitySettings);
services.AddSingleton(Options.Create(identitySettings));

services.AddAutoMapper(typeof(Program));

services.AddScoped<AuthService>();
services.AddScoped<IdentityServerService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
