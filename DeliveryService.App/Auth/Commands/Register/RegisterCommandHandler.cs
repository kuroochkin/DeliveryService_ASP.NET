using DeliveryService.App.Auth.Common;
using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.Domain.Courier;
using DeliveryService.Domain.Customer;
using DeliveryService.Domain.Manager;
using DeliveryService.Domain.User;
using ErrorOr;
using MediatR;

namespace DeliveryService.App.Auth.Commands.Register;

public class RegisterCommandHandler
    : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    public RegisterCommandHandler(
       IUnitOfWork unitOfWork,
       IJwtTokenGenerator jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle
        (RegisterCommand request,
        CancellationToken cancellationToken)
    {
        //Проверяем валидность E-Mail
        if (await _unitOfWork.Users.FindUserByEmail(request.Email) is not null)
        {
            return Errors.Auth.EmailIsWasUsed;
        }

        var role = await _unitOfWork.Roles.FindRoleByName(request.Role);

        // Хэширование пароля
        var passwordHashed = PasswordHasher.HashPassword(request.Password);

		//Создаем объект пользователя и определяем его роль
		var user = new UserEntity(
            request.LastName, 
            request.FirstName,
			passwordHashed, 
            request.Email, 
            role);


        //Добавляем пользователя в коллекцию
        await _unitOfWork.Users.Add(user);

        if (user.GetTypeUser == "Courier")
        {
            var courier = new CourierEntity(user.Id, user.LastName, user.FirstName);
            await _unitOfWork.Couriers.Add(courier);
        }
        if (user.GetTypeUser == "Customer")
        {
			var customer = new CustomerEntity(user.Id, user.LastName, user.FirstName);
			await _unitOfWork.Customers.Add(customer);
		}
		else
        {
            var manager = new ManagerEntity(user.Id, user.LastName, user.FirstName);
            await _unitOfWork.Managers.Add(manager);
        }
        await _unitOfWork.CompleteAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(token, user.GetUserTypeToString());
    }
}
