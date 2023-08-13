using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.StorageFile;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class StorageFileRepository : GenericRepository<StorageFileEntity>, IStorageFileRepository
{
	public StorageFileRepository(ApplicationDbContext context) : base(context)
	{
	}
}
