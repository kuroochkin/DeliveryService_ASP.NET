using DeliveryService.Domain.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
	public void Configure(EntityTypeBuilder<UserEntity> builder)
	{
		builder.ToTable("Users");
		builder.HasKey(user => user.Id);
		builder.Property(user => user.LastName);
		builder.Property(user => user.FirstName);
		builder.Property(user => user.Email);
		builder.Property(user => user.Password).IsRequired();
		builder.Property(user => user.Type);
	}
}
