using DeliveryService.Services.RestaurantAPI.Domain.Manager;
using DeliveryService.Services.RestaurantAPI.Domain.Restaurant;
using DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public DbSet<ManagerEntity> Managers { get; set; }
	public DbSet<RestaurantEntity> Restaurants { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new ManagerConfiguration());
		builder.ApplyConfiguration(new RestaurantConfiguration());

		base.OnModelCreating(builder);
	}
}
