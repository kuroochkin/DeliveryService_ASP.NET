using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class Customer
	{
		public static Error InvalidId => Error.Validation(
			code: "Customer.InvalidId",
			description: "Id заказчика не найден.");

		public static Error NotFound => Error.NotFound(
			code: "Customer.NotFound",
			description: "Заказчик не найден.");

		public static Error CouldNotSave => Error.Conflict(
			code: "Customer.CouldNotSave",
			description: "Заказчик не был сохранен.");
	}
}

