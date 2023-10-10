using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.Restaraunt;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class RestaurantConfiguration : IEntityTypeConfiguration<RestaurantEntity>
{
	public void Configure(EntityTypeBuilder<RestaurantEntity> builder)
	{
		builder.ToTable("Restaurants");
		builder.HasKey(restaraunt => restaraunt.Id);
		builder.Property(restaraunt => restaraunt.Name);
		builder.Property(restaraunt => restaraunt.Address);
		builder.Property(restaraunt => restaraunt.Status);
	}
}
