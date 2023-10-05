using DeliveryService.Services.PaymentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.PaymentAPI.DbContexts;

public class ApplicationDbContext : DbContext
{
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	public DbSet<PaymentEntity> Payments { get; set; }
}
