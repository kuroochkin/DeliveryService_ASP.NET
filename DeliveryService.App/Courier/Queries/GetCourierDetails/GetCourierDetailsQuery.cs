using ErrorOr;
using MediatR;

namespace DeliveryService.App.Courier.Queries.GetCourierDetails;

public record GetCourierDetailsQuery(
	string CourierId) : IRequest<ErrorOr<CourierDetailsVm>>;
