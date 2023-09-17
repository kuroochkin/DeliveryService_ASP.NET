using DeliveryService.Domain;
using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.Manager;
using DeliveryService.Domain.Order;
using DeliveryService.Domain.PaymentOrder;
using DeliveryService.Domain.Product;
using DeliveryService.Domain.Restaraunt;
using DeliveryService.Domain.Role;
using DeliveryService.Domain.User;
using DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public DbSet<UserEntity> Users { get; set; }
	public DbSet<RoleEntity> Roles { get; set; }
	public DbSet<CourierEntity> Couriers { get; set; }
	public DbSet<CustomerEntity> Customers { get; set; }
	public DbSet<ManagerEntity> Managers { get; set; }
	public DbSet<RestaurantEntity> Restaurants { get; set; }
	public DbSet<OrderEntity> Orders { get; set; }
	public DbSet<PaymentOrderEntity> OrderPayments { get; set; }
	public DbSet<ProductEntity> Products { get; set; }
	public DbSet<OrderItemEntity> OrderItems { get; set; }
	public DbSet<SectionEntity> Sections { get; set; }


	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new RoleConfiguration());
		builder.ApplyConfiguration(new CourierConfiguration());
		builder.ApplyConfiguration(new CustomerConfiguration());
		builder.ApplyConfiguration(new ManagerConfiguration());
		builder.ApplyConfiguration(new OrderConfiguration());
		builder.ApplyConfiguration(new PaymentOrderConfiguration());
		builder.ApplyConfiguration(new ProductConfiguration());
		builder.ApplyConfiguration(new UserConfiguration());
		builder.ApplyConfiguration(new OrderItemConfiguration());
		builder.ApplyConfiguration(new SectionConfiguration());
		builder.ApplyConfiguration(new RestaurantConfiguration());


		base.OnModelCreating(builder);
	}
}
