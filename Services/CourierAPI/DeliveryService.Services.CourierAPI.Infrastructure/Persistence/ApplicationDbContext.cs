using DeliveryService.Services.CourierAPI.Domain.Courier;
using DeliveryService.Services.CourierAPI.Infrastructure.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.CourierAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public DbSet<CourierEntity> Couriers { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new CourierConfiguration());

		base.OnModelCreating(builder);
	}
}
