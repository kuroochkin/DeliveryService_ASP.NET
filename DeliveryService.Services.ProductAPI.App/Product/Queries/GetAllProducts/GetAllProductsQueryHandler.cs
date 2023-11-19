using DeliveryService.Services.ProductAPI.App.Common.Interfaces;
using DeliveryService.Services.ProductAPI.App.Vm.Product;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Product.Queries.GetAllProducts;

public class GetAllProductsQueryHandler
	: IRequestHandler<GetAllProductsQuery, ErrorOr<ProductsVm>>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IUserContext _userContext;

    public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IUserContext userContext)
    {
        _unitOfWork = unitOfWork;
        _userContext = userContext;
    }
    public async Task<ErrorOr<ProductsVm>> Handle(
		GetAllProductsQuery request,
		CancellationToken cancellationToken)
	{
		var userId = _userContext.CurrentUserId;

		var products = await _unitOfWork.Products.GetAllProducts();
		if (products is null)
		{
			return new ErrorOr<ProductsVm>();
		}

		var allProducts = products.Select(product => new ProductDetailsVm(
		product.Id.ToString(),
		product.Title,
		product.Price.ToString(),
		product.Thumbnail,
		product.Section.Name
		)).ToList();

		var allProd = new ProductsVm(allProducts);

		return allProd;
	}
}
