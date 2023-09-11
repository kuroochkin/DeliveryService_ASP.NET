using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using static DeliveryService.App.Common.Errors.Errors;

namespace DeliveryService.App.Product.Queries.GetProductDetails;

public class GetProductsDetailsQueryHandler
	: IRequestHandler<GetProductsDetailsQuery, ErrorOr<ProductDetailsVm>>
{
	private readonly IUnitOfWork _unitOfWork;

	public GetProductsDetailsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}
	public async Task<ErrorOr<ProductDetailsVm>> Handle(
		GetProductsDetailsQuery request, 
		CancellationToken cancellationToken)
	{
		if (!int.TryParse(request.ProductId, out var productId))
		{
			return Errors.Product.InvalidId;
		}

		var _product = await _unitOfWork.Products.FindProductById(productId);
		if (_product is null)
		{
			return new ErrorOr<ProductDetailsVm>();
		}

		var product = new ProductDetailsVm(
		_product.Id.ToString(),
		_product.Title,
		_product.Price.ToString(),
		_product.Thumbnail,
		_product.Section.Name
		);

		return product;
	}
}
