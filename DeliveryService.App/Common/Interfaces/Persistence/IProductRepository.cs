using DeliveryService.Domain.Order;
using DeliveryService.Domain.Product;

namespace DeliveryService.App.Common.Interfaces.Persistence;

public interface IProductRepository : IGenericRepository<ProductEntity>
{
}
