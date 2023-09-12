using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Customer.Queries.GetCustomerDetails;

public class GetCustomerDetailsQueryHandler
	: IRequestHandler<GetCustomerDetailsQuery, ErrorOr<CustomerDetailsVm>>
{

	private readonly IUnitOfWork _unitOfWork;
	public GetCustomerDetailsQueryHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<CustomerDetailsVm>> Handle(
		GetCustomerDetailsQuery request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CustomerId, out var customerId))
		{
			return Errors.Customer.InvalidId;
		}

		var customer = await _unitOfWork.Customers.FindById(customerId);
		if(customer is null)
		{
			return Errors.Customer.NotFound;
		}

		var user = await _unitOfWork.Users.FindById(customerId);
		if(user is null)
		{
			return Errors.User.NotFound;
		}

		var customerDetails = new CustomerDetailsVm(
			customer.Id.ToString(),
			user.Email,
			user.Password,
			user.LastName,
			user.FirstName,
			customer.BirthDay,
			customer.CountOrder);

		return customerDetails;
	}
}
