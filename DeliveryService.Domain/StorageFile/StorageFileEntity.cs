namespace DeliveryService.Domain.StorageFile;

public class StorageFileEntity
{
	public Guid FileId { get; set; }

	public int Length { get; set; }

	public string BucketName { get; set; }

	public string FileName { get; set; }

	public string FilePath { get; set; }
}
