using DeliveryService.Services.ProductAPI.App.Common.Errors;
using DeliveryService.Services.ProductAPI.App.Common.Interfaces;
using DeliveryService.Services.ProductAPI.App.Vm.Product;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Product.Queries.GetProductsDetails;

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
