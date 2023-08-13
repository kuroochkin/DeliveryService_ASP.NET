using Amazon.S3;
using DeliveryService.App.Common.Interfaces.Minio;
using Minio.DataModel;

namespace DeliveryService.infrastructure.Minio;

public class StorageService : IStorageService
{
	private readonly AmazonS3Client _S3client;

	public StorageService(AmazonS3Client S3client)
	{
		_S3client = S3client;
	}

	public async Task<Stream> GetStreamAsync(Guid fileId, Bucket bucket)
	{
		using var storageFile = await _S3client.GetObjectAsync(
			bucket.Name,
			fileId.ToString());

		await using var responseStream = storageFile.ResponseStream;
		var ms = new MemoryStream();
		await responseStream.CopyToAsync(ms);
		if(ms.CanSeek) ms.Seek(0, SeekOrigin.Begin);
		return ms;
	}
}
