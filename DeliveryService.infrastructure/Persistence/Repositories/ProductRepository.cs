using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Product;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
	public ProductRepository(ApplicationDbContext context) : base(context)
	{
	}
}
