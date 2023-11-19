using DeliveryService.Services.CourierAPI.App.Common.Interfaces.Persistence;
using DeliveryService.Services.CourierAPI.Domain.Courier;

namespace DeliveryService.Services.CourierAPI.Infrastructure.Persistence.Repositories;

public class CourierRepository : GenericRepository<CourierEntity>, ICourierRepository
{
	public CourierRepository(ApplicationDbContext context) : base(context)
	{
	}
}
