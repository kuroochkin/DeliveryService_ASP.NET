using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.Role;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class RoleConfiguration : IEntityTypeConfiguration<RoleEntity>
{
	public void Configure(EntityTypeBuilder<RoleEntity> builder)
	{
		builder.ToTable("Roles");
		builder.HasKey(role => role.Id);
		builder.Property(role => role.Name);
	}
}

