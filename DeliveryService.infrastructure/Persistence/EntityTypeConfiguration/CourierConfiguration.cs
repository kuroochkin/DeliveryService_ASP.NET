using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class CourierConfiguration : IEntityTypeConfiguration<CourierEntity>
{
	public void Configure(EntityTypeBuilder<CourierEntity> builder)
	{
		builder.ToTable("Couriers");
		builder.HasKey(courier => courier.Id);
		builder.Property(courier => courier.LastName);
		builder.Property(courier => courier.FirstName);
		builder.Property(courier => courier.Patronymic);
		builder.Property(courier => courier.BirthDay);
		builder.Property(courier => courier.CountOrder);
	}
}
