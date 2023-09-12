using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Customer.Commands.EditProfile;

public class EditProfileCommandHandler
	: IRequestHandler<EditProfileCommand, ErrorOr<bool>>
{
	private readonly IUnitOfWork _unitOfWork;

	public EditProfileCommandHandler(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<ErrorOr<bool>> Handle(
		EditProfileCommand request, 
		CancellationToken cancellationToken)
	{
		if (!Guid.TryParse(request.CustomerId, out var customerId))
		{
			return Errors.Customer.InvalidId;
		}

		var customer = await _unitOfWork.Customers.FindById(customerId);
		if (customer is null)
		{
			return Errors.Customer.NotFound;
		}

		var user = await _unitOfWork.Users.FindById(customerId);
		if (user is null)
		{
			return Errors.User.NotFound;
		}

		user.FirstName = request.FirstName;
		user.LastName = user.LastName;
		user.Email = request.Email;
		user.Password = request.Password;

		customer.FirstName = request.FirstName;
		customer.LastName = request.LastName;
		customer.BirthDay = request.Birthday;

		return await _unitOfWork.CompleteAsync();
	}
}
