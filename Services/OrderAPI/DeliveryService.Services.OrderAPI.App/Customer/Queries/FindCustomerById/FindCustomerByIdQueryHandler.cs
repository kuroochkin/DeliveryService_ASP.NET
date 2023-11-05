using DeliveryService.Services.OrderAPI.App.Common.Errors;
using DeliveryService.Services.OrderAPI.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace DeliveryService.Services.OrderAPI.App.Customer.Queries.FindCustomerById;

public class FindCustomerByIdQueryHandler
	: IRequestHandler<FindCustomerByIdQuery, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public FindCustomerByIdQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle(
		FindCustomerByIdQuery request,
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CustomerId, out var customerId))
		{
			return Errors.Customer.InvalidId;
		}

		var customer = await _unitOfWork.Customers.FindById(customerId);
		if (customer is null)
		{
			return false;
		}

		return true;
	}
}
