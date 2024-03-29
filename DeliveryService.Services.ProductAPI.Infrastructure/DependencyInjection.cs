﻿using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.infrastructure.Auth;
using DeliveryService.Services.ProductAPI.App.Common.Interfaces;
using DeliveryService.Services.ProductAPI.Infrastructure.Persistence;
using DeliveryService.Services.ProductAPI.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DeliveryService.Services.ProductAPI.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services,
		ConfigurationManager configuration)
	{
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<ISectionRepository, SectionRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();

		services.AddAuth(configuration);

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

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidAudience = "audience",
                    ValidIssuer = "issuer",
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes("ssdfeuihewjsdfdklmfdbhgqqwpognmbnfopwe123/tfdgerfdethyg")
                    )
                };
            });

        //services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
        //	.AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
        //	{
        //		ValidateIssuer = true,
        //		ValidateAudience = true,
        //		ValidateLifetime = true,
        //		ValidateIssuerSigningKey = true,
        //		ValidIssuer = jwtSettings.Issuer,
        //		ValidAudience = jwtSettings.Audience,
        //		IssuerSigningKey = new SymmetricSecurityKey(
        //			Encoding.UTF8.GetBytes(jwtSettings.Secret))
        //	});

        //     services.AddAuthentication("Bearer")
        //.AddIdentityServerAuthentication("Bearer", options =>
        //{
        //	options.ApiName = "ProductAPI";
        //	options.Authority = "http://localhost:5007";
        //	options.RequireHttpsMetadata = false;
        //});



        //      services.AddAuthentication(config =>
        //{
        //	config.DefaultAuthenticateScheme =
        //		JwtBearerDefaults.AuthenticationScheme;
        //	config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer("Bearer", options =>
        //	{
        //		options.Authority = "http://localhost:5007";
        //		options.Audience = "ProductAPI";
        //		options.RequireHttpsMetadata = false;
        //	});

        return services;
	}
}
