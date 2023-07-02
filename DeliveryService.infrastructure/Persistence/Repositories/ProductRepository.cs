using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Order;
using DeliveryService.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.infrastructure.Persistence.Repositories;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
	public ProductRepository(ApplicationDbContext context) : base(context)
	{
		
	}

	public async Task<ProductEntity?> FindProductById(int id)
	{
		return await _context.Products
			.FirstOrDefaultAsync(product => product.Id == id);
	}

	public async Task<List<ProductEntity>?> GetAllProducts()
	{
		return await _context.Products
			.Include(product => product.Section)
			.ToListAsync();
	}

	public async Task<List<ProductEntity>?> GetProductsBySection(Guid id)
	{
		return await _context.Products
			.Include(product => product.Section)
			.Where(product => product.Section.Id == id)
			.ToListAsync();
	}
}
