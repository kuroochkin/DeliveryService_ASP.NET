using DeliveryService.App.Common.Errors;
using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.OrderAPI.App.Order.Queries.FindOrderById;

public class FindOrderByIdQueryHandler
	: IRequestHandler<FindOrderByIdQuery, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public FindOrderByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle(
		FindOrderByIdQuery request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.OrderId, out var orderId))
		{
			return Errors.Order.InvalidId;
		}

		var order = await _unitOfWork.Orders.FindById(orderId);
		if (order is null)
		{
			return false;
		}

		return true;
	}
}
