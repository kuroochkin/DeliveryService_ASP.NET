using DeliveryService.Services.CourierAPI.Domain.Courier;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.Services.CourierAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class CourierConfiguration : IEntityTypeConfiguration<CourierEntity>
{
	public void Configure(EntityTypeBuilder<CourierEntity> builder)
	{
		builder.HasKey(courier => courier.Id);
		builder.Property(courier => courier.LastName);
		builder.Property(courier => courier.FirstName);
		builder.Property(courier => courier.Patronymic);
		builder.Property(courier => courier.BirthDay);
		builder.Property(courier => courier.CountOrder);
	}
}
