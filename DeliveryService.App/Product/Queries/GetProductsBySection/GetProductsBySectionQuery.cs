using ErrorOr;
using MediatR;

namespace DeliveryService.App.Product.Queries.GetProductsBySection;

public record GetProductsBySectionQuery(string SectionId) : IRequest<ErrorOr<ProductsVm>>;

