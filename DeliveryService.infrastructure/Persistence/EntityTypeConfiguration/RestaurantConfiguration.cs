using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.Restaraunt;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class RestaurantConfiguration : IEntityTypeConfiguration<RestarauntEntity>
{
	public void Configure(EntityTypeBuilder<RestarauntEntity> builder)
	{
		builder.ToTable("Restaraunts");
		builder.HasKey(restaraunt => restaraunt.Id);
		builder.Property(restaraunt => restaraunt.Name);
		builder.Property(restaraunt => restaraunt.Address);
		builder.Property(restaraunt => restaraunt.Status);

	}
}
