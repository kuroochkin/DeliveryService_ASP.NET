using DeliveryService.Services.CourierAPI.App.Courier.Vm;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.CourierAPI.App.Courier.Queries.GetCourierDetails;

public record GetCourierDetailsQuery(
    string CourierId) : IRequest<ErrorOr<CourierDetailsVm>>;
