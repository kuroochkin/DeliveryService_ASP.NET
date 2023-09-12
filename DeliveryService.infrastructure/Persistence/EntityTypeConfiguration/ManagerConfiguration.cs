using DeliveryService.Domain.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class ManagerConfiguration : IEntityTypeConfiguration<ManagerEntity>
{
	public void Configure(EntityTypeBuilder<ManagerEntity> builder)
	{
		builder.ToTable("Managers");
		builder.HasKey(manager => manager.Id);
		builder.Property(manager => manager.LastName);
		builder.Property(manager => manager.FirstName);
	}
}
