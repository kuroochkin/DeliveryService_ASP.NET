﻿using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.Order;
using DeliveryService.Domain.Product;
using DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DeliveryService.infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public DbSet<CourierEntity> Couriers { get; set; }
	public DbSet<CustomerEntity> Customers { get; set; }
	public DbSet<OrderEntity> Orders { get; set; }
	public DbSet<ProductEntity> Products { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new CourierConfiguration());
		builder.ApplyConfiguration(new CustomerConfiguration());
		builder.ApplyConfiguration(new OrderConfiguration());
		builder.ApplyConfiguration(new ProductConfiguration());
		
		base.OnModelCreating(builder);
	}
}