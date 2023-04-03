using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class Auth
	{
		public static Error EmailIsWasUsed => Error.Validation(
			code: "User.InvalidEmail",
			description: "Такой Email уже используется!");

		public static Error InvalidCredentials => Error.Validation(
			   code: "Auth.InvalidCred",
			   description: "Invalid credentials.");

	}
}
