using DeliveryService.Services.ProductAPI.App.Common.Errors;
using DeliveryService.Services.ProductAPI.App.Common.Interfaces;
using DeliveryService.Services.ProductAPI.App.Vm.Product;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Product.Queries.GetProductsBySection;

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

		var products = await _unitOfWork.Products.GetProductsBySectionId(sectionId);
		if (products is null)
		{
			return new ErrorOr<ProductsVm>();
		}

		var allProducts = products.Select(product => new ProductDetailsVm(
		product.Id.ToString(),
		product.Title,
		product.Price.ToString(),
		product.Thumbnail,
		product?.Section?.Name
		)).ToList();

		var allProd = new ProductsVm(allProducts);

		return allProd;
	}
}
