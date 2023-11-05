using DeliveryService.Services.OrderAPI.Domain.Order;
using DeliveryService.Services.OrderAPI.Domain.OrderItem;
using DeliveryService.Services.OrderAPI.Infrastructure.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{ 
	public DbSet<OrderEntity> Orders { get; set; }
	public DbSet<OrderItemEntity> OrderItems { get; set; }


	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new OrderConfiguration());
		builder.ApplyConfiguration(new OrderItemConfiguration());

		base.OnModelCreating(builder);
	}
}
