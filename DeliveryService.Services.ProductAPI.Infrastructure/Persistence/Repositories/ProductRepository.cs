using DeliveryService.Services.ProductAPI.App.Common.Interfaces;
using DeliveryService.Services.ProductAPI.Domain.Product;
using Microsoft.EntityFrameworkCore;

namespace DeliveryService.Services.ProductAPI.Infrastructure.Persistence.Repositories;

public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository
{
	public ProductRepository(ApplicationDbContext context) : base(context)
	{
	}

	public async Task<ProductEntity?> FindProductById(int id)
	{
		return await _context.Products
			.Include(product => product.Section)
			.FirstOrDefaultAsync(product => product.Id == id);
	}

	public async Task<List<ProductEntity>?> GetAllProducts()
	{
		return await _context.Products
			.Include(product => product.Section)
			.ToListAsync();
	}

	public async Task<List<ProductEntity>?> GetProductsBySectionId(Guid id)
	{
		return await _context.Products
			.Include(product => product.Section)
			.Where(product => product.Section.Id == id)
			.ToListAsync();
	}
}
