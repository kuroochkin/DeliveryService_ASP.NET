using DeliveryService.Services.RestaurantAPI.Domain.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class ManagerConfiguration : IEntityTypeConfiguration<ManagerEntity>
{
	public void Configure(EntityTypeBuilder<ManagerEntity> builder)
	{
		builder.HasKey(manager => manager.Id);
		builder.Property(manager => manager.LastName);
		builder.Property(manager => manager.FirstName);
		builder.Property(manager => manager.CountOrder);
	}
}
