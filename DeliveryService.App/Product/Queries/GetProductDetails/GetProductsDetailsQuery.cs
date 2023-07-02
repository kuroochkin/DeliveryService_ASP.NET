using ErrorOr;
using MediatR;


namespace DeliveryService.App.Product.Queries.GetProductDetails;

public record GetProductsDetailsQuery(string ProductId) : IRequest<ErrorOr<ProductDetailsVm>>;