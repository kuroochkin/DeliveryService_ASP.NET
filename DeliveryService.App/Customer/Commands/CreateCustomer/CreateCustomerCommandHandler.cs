using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Customer.Commands.CreateCustomer;

public class CreateCustomerCommandHandler
	 : IRequestHandler<СreateCustomerCommand, ErrorOr<bool>>
{

	private readonly IUnitOfWork _unitOfWork;

	public CreateCustomerCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle
		(СreateCustomerCommand request, 
		CancellationToken cancellationToken)
	{
		var customer = new CustomerEntity(
			request.LastName,
			request.FirstName,
			request.Patromymic);

		if (await _unitOfWork.Customers.Add(customer))
		{
			return await _unitOfWork.CompleteAsync();
		}

		return false;
	}
}
