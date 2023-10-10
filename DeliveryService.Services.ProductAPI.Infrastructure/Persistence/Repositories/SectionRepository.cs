using DeliveryService.Services.ProductAPI.App.Common.Interfaces;
using DeliveryService.Services.ProductAPI.Domain.Section;

namespace DeliveryService.Services.ProductAPI.Infrastructure.Persistence.Repositories;

public class SectionRepository : GenericRepository<SectionEntity>, ISectionRepository
{
	public SectionRepository(ApplicationDbContext context) : base(context)
	{
	}
}
