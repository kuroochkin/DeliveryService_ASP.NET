using DeliveryService.App.Auth.Common;
using DeliveryService.App.Common.Errors;
using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.App.Common.Interfaces.Persistence;
using DeliveryService.App.Common.Messages;
using DeliveryService.App.Common.RabbitMQSender;
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
    private readonly IRabbitMQRegistrationMessageSender _rabbitMQSender;

    public RegisterCommandHandler(
       IUnitOfWork unitOfWork,
       IJwtTokenGenerator jwtTokenGenerator,
	   IRabbitMQRegistrationMessageSender rabbitMQMessage)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenGenerator = jwtTokenGenerator;
        _rabbitMQSender = rabbitMQMessage;
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

        var message = new EmailServiceDTO()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        _rabbitMQSender.SendMessage(message, "email-queue");

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(token, user.GetUserTypeToString());
    }
}
