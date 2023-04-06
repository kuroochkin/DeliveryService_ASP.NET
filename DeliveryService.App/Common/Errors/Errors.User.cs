using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class User
	{
		public static Error InvalidId => Error.Validation(
			code: "User.InvalidId",
			description: "Id пользователя не найден.");
	}
}

