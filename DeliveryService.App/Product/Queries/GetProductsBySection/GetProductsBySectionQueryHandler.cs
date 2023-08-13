using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Product.Queries.GetAllProducts;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Product.Queries.GetProductsBySection;

public class GetProductsBySectionQueryHandler
	: IRequestHandler<GetProductsBySectionQuery, ErrorOr<ProductsVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetProductsBySectionQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<ProductsVm>> Handle(
		GetProductsBySectionQuery request,
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.SectionId, out var sectionId))
		{
			return Errors.Section.InvalidId;
		}

		var products = await _unitOfWork.Products.GetProductsBySection(sectionId);
		if (products is null)
		{
			return new ErrorOr<ProductsVm>();
		}

		var allProducts = products.Select(product => new ProductDetailsVm(
		product.Id.ToString(),
		product.Title,
		product.Price.ToString(),
		product.StorageFile.FileId.ToString(),
		product?.Section?.Name
		)).ToList();

		var allProd = new ProductsVm(allProducts);

		return allProd;
	}
}


