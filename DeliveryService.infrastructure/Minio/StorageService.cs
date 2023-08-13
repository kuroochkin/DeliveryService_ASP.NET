using DeliveryService.App.Common.Interfaces.Minio;
using Microsoft.Extensions.Options;

namespace DeliveryService.infrastructure.Minio;

public class StorageService : IStorageService
{
	private readonly MinioSettings _minioSettings;

	public StorageService(IOptions<MinioSettings> minioSettings)
	{
		_minioSettings = minioSettings.Value;
	}

	public void GetBucket(string bucketName)
	{
		
	}
}
