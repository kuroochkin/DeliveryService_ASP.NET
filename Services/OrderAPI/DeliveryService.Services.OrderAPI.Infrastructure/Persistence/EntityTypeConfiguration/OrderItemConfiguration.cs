using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Services.OrderAPI.Domain.OrderItem;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
	public void Configure(EntityTypeBuilder<OrderItemEntity> builder)
	{
		builder.HasKey(item => item.Id);

		builder.Property(item => item.Count);
		builder.Property(item => item.TotalPrice);
	}
}
