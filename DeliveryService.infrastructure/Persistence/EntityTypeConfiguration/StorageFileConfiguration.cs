using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeliveryService.Domain.StorageFile;

namespace DeliveryService.infrastructure.Persistence.EntityTypeConfiguration;


public class StorageFileConfiguration : IEntityTypeConfiguration<StorageFileEntity>
{
	public void Configure(EntityTypeBuilder<StorageFileEntity> builder)
	{
		builder.ToTable("StorageFiles");
		builder.HasKey(file => file.FileId);
		builder.Property(file => file.FilePath);
		builder.Property(file => file.BucketName);
		builder.Property(file => file.Length);
		builder.Property(file => file.FileName);
	}
}