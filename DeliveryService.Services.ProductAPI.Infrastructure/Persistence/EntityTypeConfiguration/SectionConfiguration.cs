using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Services.ProductAPI.Domain.Section;

namespace DeliveryService.Services.ProductAPI.Infrastructure.Persistence.EntityTypeConfiguration;

public class SectionConfiguration : IEntityTypeConfiguration<SectionEntity>
{
	public void Configure(EntityTypeBuilder<SectionEntity> builder)
	{
		builder.HasKey(section => section.Id);
		builder.Property(section => section.Name);
	}
}
