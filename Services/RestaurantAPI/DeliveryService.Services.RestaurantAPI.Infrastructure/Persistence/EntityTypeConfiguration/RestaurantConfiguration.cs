using DeliveryService.Services.RestaurantAPI.Domain.Restaurant;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Services.RestaurantAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class RestaurantConfiguration : IEntityTypeConfiguration<RestaurantEntity>
{
	public void Configure(EntityTypeBuilder<RestaurantEntity> builder)
	{
		builder.HasKey(restaraunt => restaraunt.Id);
		builder.Property(restaraunt => restaraunt.Name);
		builder.Property(restaraunt => restaraunt.Address);
		builder.Property(restaraunt => restaraunt.Status);
	}
}
