using ErrorOr;

namespace DeliveryService.Services.RestaurantAPI.App.Common.Errors;

public static partial class Errors
{
	public static class Restaurant
	{
		public static Error InvalidId => Error.Validation(
			code: "Restaurant.InvalidId",
			description: "Id ресторана не найдено.");

		public static Error NotFound => Error.NotFound(
			code: "Restaurant.NotFound",
			description: "Ресторан не найден.");

		public static Error CouldNotSave => Error.Conflict(
			code: "Restaurant.CouldNotSave",
			description: "Ресторан не был сохранен.");
	}
}
