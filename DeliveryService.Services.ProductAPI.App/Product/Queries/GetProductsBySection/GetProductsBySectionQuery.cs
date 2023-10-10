using DeliveryService.Services.ProductAPI.App.Vm.Product;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.ProductAPI.App.Product.Queries.GetProductsBySection;

public record GetProductsBySectionQuery(string SectionId) : IRequest<ErrorOr<ProductsVm>>;
