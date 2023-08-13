using Minio.DataModel;

namespace DeliveryService.App.Common.Interfaces.Minio;

public interface IStorageService
{
	Task<Stream> GetStreamAsync(Guid fileId, Bucket bucket);
}
