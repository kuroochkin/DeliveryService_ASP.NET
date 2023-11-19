using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Services.OrderAPI.Domain.Order;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
	public void Configure(EntityTypeBuilder<OrderEntity> builder)
	{
		builder.HasKey(order => order.Id);

		builder.Property(order => order.CourierId);
		builder.Property(order => order.ManagerId);

		builder.Property(order => order.Created);
		builder.Property(order => order.End);
		builder.Property(order => order.Status);

		builder.Property(order => order.Id)
		   .IsRequired()
		   .ValueGeneratedNever();

		builder.HasMany(item => item.OrderItems);
	}
}
