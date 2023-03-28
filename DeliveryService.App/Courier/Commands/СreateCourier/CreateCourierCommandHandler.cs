using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Courier;
using ErrorOr;
using MediatR;
using static DeliveryService.App.Common.Errors.Errors;

namespace DeliveryService.App.Courier.Commands.СreateCourier;

public class CreateCourierCommandHandler
	: IRequestHandler<СreateCourierCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public  CreateCourierCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle(СreateCourierCommand request, CancellationToken cancellationToken)
	{
		//var courier = new CourierEntity(
		//	request.LastName,
		//	request.FirstName,
		//	request.Patromymic);

		//if (await _unitOfWork.Couriers.Add(courier))
		//{
		//	return await _unitOfWork.CompleteAsync();
		//}

		return false;

	}
}
