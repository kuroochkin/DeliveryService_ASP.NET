using DeliveryService.App.Order.Queries.GetOrdersUser;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Product.Queries.GetAllProducts;

public record GetAllProductsQuery() : IRequest<ErrorOr<ProductsVm>>;
