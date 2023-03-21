using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Courier;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class CourierRepository : GenericRepository<CourierEntity>, ICourierRepository
{
	public CourierRepository(ApplicationDbContext context) : base(context)
	{
	}
}
