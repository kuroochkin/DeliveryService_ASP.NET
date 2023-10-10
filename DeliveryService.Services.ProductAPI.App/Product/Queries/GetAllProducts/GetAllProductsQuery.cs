using DeliveryService.Services.ProductAPI.App.Vm.Product;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Product.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<ErrorOr<ProductsVm>>;
