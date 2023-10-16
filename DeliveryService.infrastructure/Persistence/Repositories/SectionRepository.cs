using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Section;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class SectionRepository : GenericRepository<SectionEntity>, ISectionRepository
{
	public SectionRepository(ApplicationDbContext context) : base(context)
	{
	}
}
