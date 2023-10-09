using DeliveryService.Services.PaymentAPI.Domain.Payment;
using DeliveryService.Services.PaymentAPI.Infrastructure.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.PaymentAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public DbSet<PaymentEntity> Payments { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new PaymentConfiguration());

		base.OnModelCreating(builder);
	}
}
