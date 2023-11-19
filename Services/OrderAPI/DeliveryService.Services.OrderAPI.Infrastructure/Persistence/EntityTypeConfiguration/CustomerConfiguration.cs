using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Services.OrderAPI.Domain.Customer;

namespace DeliveryService.Services.OrderAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
{
	public void Configure(EntityTypeBuilder<CustomerEntity> builder)
	{
		builder.ToTable("Customers");
		builder.HasKey(customer => customer.Id);
		builder.Property(customer => customer.LastName);
		builder.Property(customer => customer.FirstName);
		builder.Property(customer => customer.BirthDay);
		builder.Property(courier => courier.CountOrder);
	}
}
