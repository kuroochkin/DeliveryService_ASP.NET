using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class User
	{
		public static Error InvalidId => Error.Validation(
			code: "User.InvalidId",
			description: "Id пользователя не найден.");

		public static Error NotFound => Error.Validation(
			code: "User.NotFound",
			description: "Пользователь с таким Id не найден.");
	}
}

