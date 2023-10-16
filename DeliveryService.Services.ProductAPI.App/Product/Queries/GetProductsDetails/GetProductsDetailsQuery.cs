using DeliveryService.Services.ProductAPI.App.Vm.Product;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Product.Queries.GetProductsDetails;

public record GetProductsDetailsQuery(string ProductId) : IRequest<ErrorOr<ProductDetailsVm>>;