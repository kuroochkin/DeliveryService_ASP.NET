using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class Order
	{
		public static Error InvalidId => Error.Validation(
			code: "Order.InvalidId",
			description: "Id заказа не найден.");

		public static Error NotFound => Error.NotFound(
			code: "Order.NotFound",
			description: "Заказ не найден.");

		public static Error CouldNotSave => Error.Conflict(
			code: "Order.CouldNotSave",
			description: "Заказ не был сохранен.");
	}
}
