using DeliveryService.App.Auth.Common;
using DeliveryService.App.Common.Interfaces.Auth;
using DeliveryService.App.Common.Interfaces.Persistence;
using ErrorOr;
using MediatR;
using DeliveryService.App.Common.Errors;

namespace DeliveryService.App.Auth.Queries.Login
{
	public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IJwtTokenGenerator _jwtTokenGenerator;

		public LoginQueryHandler(
			IUnitOfWork unitOfWork,
			IJwtTokenGenerator jwtTokenGenerator)
		{
			_unitOfWork = unitOfWork;
			_jwtTokenGenerator = jwtTokenGenerator;
		}

		public async Task<ErrorOr<AuthenticationResult>> Handle
			(LoginQuery request, 
			CancellationToken cancellationToken)
		{
			var user = await _unitOfWork.Users.FindUserByRegisteredEmail(request.Email);
			if(user is not null)
			{
				if(user.Password == request.Password)
				{
					var token = _jwtTokenGenerator.GenerateToken(user);
					return new AuthenticationResult(token, user.GetUserTypeToString);
				}
			}
			return Errors.Auth.InvalidCredentials;
		}
	}
}
