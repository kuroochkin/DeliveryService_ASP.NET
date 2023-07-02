using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;

public class SectionConfiguration : IEntityTypeConfiguration<SectionEntity>
{
	public void Configure(EntityTypeBuilder<SectionEntity> builder)
	{
		builder.ToTable("Sections");
		builder.HasKey(section => section.Id);
		builder.Property(section => section.Name);
	}
}