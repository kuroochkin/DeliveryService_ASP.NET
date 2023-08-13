using DeliveryService.Domain.Product;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
	public void Configure(EntityTypeBuilder<ProductEntity> builder)
	{
		builder.ToTable("Products");

		builder.HasKey(product => product.Id);

		builder.Property(product => product.Title);
		builder.Property(product => product.Price);
		
		builder.HasOne(product => product.StorageFile);
		builder.HasOne(product => product.Section);
	}
}
