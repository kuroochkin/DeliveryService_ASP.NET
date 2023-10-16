using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Services.ProductAPI.Domain.Product;

namespace DeliveryService.Services.ProductAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
	public void Configure(EntityTypeBuilder<ProductEntity> builder)
	{

		builder.HasKey(product => product.Id);

		builder.Property(product => product.Title);
		builder.Property(product => product.Price);
		builder.Property(product => product.Thumbnail);
		builder.Property(product => product.RestaurantId);

		builder.HasOne(product => product.Section);
	}
}
