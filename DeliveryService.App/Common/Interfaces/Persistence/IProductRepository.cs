using DeliveryService.Domain.Product;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IProductRepository : IGenericRepository<ProductEntity>
{
	Task<ProductEntity?> FindProductById(int id);
	Task<List<ProductEntity>?> GetAllProducts();
	Task<List<ProductEntity>?> GetProductsBySection(Guid id);
}
