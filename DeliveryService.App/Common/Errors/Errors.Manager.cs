using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class Manager
	{
		public static Error InvalidId => Error.Validation(
			code: "Manager.InvalidId",
			description: "Id менеджера не найден.");

		public static Error NotFound => Error.NotFound(
			code: "Manager.NotFound",
			description: "Менеджер не найден.");

		public static Error CouldNotSave => Error.Conflict(
			code: "Manager.CouldNotSave",
			description: "Менеджер не был сохранен.");
	}
}
