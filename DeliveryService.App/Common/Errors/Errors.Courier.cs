using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class Courier
	{
		public static Error InvalidId => Error.Validation(
			code: "Courier.InvalidId",
			description: "Id заказчика не найден.");

		public static Error NotFound => Error.NotFound(
			code: "Courier.NotFound",
			description: "Курьер не найден.");

		public static Error CouldNotSave => Error.Conflict(
			code: "Courier.CouldNotSave",
			description: "Курьер не был сохранен.");
	}
}


