using ErrorOr;

namespace DeliveryService.App.Common.Errors;

public static partial class Errors
{
	public static class Product
	{
		public static Error InvalidId => Error.Validation(
			code: "Product.InvalidId",
			description: "Id товара не найден.");

		public static Error NotFound => Error.NotFound(
			code: "Product.NotFound",
			description: "Товар не найден.");

		public static Error CouldNotSave => Error.Conflict(
			code: "Product.CouldNotSave",
			description: "Товар не был сохранен.");
	}
}
