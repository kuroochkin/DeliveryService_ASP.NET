using ErrorOr;
using MediatR;
using DeliveryService.App.Common.Interfaces.Persistence;

namespace DeliveryService.App.Product.Queries.GetAllProducts;

public class GetAllProductsQueryHandler
	: IRequestHandler<GetAllProductsQuery, ErrorOr<ProductsVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetAllProductsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<ProductsVm>> Handle(
		GetAllProductsQuery request, 
		CancellationToken cancellationToken)
	{
		var products = await _unitOfWork.Products.GetAll();
		if(products is null)
		{
			return new ErrorOr<ProductsVm>();
		}

		var allProducts = products.Select(product => new ProductDetailsVm(
		product.Id.ToString(),
		product.Title,
		product.Price.ToString(),
		product.Thumbnail
		)).ToList();

		var allProd = new ProductsVm(allProducts);

		return allProd;
	}
}
