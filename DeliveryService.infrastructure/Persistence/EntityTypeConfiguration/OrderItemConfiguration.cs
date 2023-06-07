using DeliveryService.Domain.Courier;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class OrderItemConfiguration: IEntityTypeConfiguration<OrderItemEntity>
{
	public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
	{
		builder.ToTable("OrderItem");
		builder.HasKey(item => item.Id);

		builder.Property(item => item.Count);
		builder.Property(item => item.TotalPrice);

	}
}
