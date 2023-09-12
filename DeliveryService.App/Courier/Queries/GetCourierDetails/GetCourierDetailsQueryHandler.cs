using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using DeliveryService.App.Common.Errors;

namespace DeliveryService.App.Courier.Queries.GetCourierDetails;

public class GetCourierDetailsQueryHandler
	: IRequestHandler<GetCourierDetailsQuery, ErrorOr<CourierDetailsVm>>
{

	private readonly IUnitOfWork _unitOfWork;
	public GetCourierDetailsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<CourierDetailsVm>> Handle(
		GetCourierDetailsQuery request,
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CourierId, out var courierId))
		{
			return Errors.Courier.InvalidId;
		}

		var courier = await _unitOfWork.Couriers.FindById(courierId);
		if (courier is null)
		{
			return Errors.Courier.NotFound;
		}

		var user = await _unitOfWork.Users.FindById(courierId);
		if (user is null)
		{
			return Errors.User.NotFound;
		}

		var courierDetails = new CourierDetailsVm(
			courier.Id.ToString(),
			user.Email,
			user.Password,
			user.LastName,
			user.FirstName,
			courier.BirthDay,
			courier.CountOrder);

		return courierDetails;
	}
}
