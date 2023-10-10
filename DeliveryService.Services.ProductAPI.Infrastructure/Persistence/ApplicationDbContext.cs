using DeliveryService.Services.ProductAPI.Domain.Product;
using DeliveryService.Services.ProductAPI.Domain.Section;
using DeliveryService.Services.ProductAPI.Infrastructure.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.ProductAPI.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
	public DbSet<ProductEntity> Products { get; set; }
	public DbSet<SectionEntity> Sections { get; set; }

	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
	   : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		builder.ApplyConfiguration(new ProductConfiguration());
		builder.ApplyConfiguration(new SectionConfiguration());

		base.OnModelCreating(builder);
	}
}
