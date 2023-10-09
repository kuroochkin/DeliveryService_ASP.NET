using ErrorOr;

namespace DeliveryService.Services.PaymentAPI.App.Common.Errors;

public static partial class Errors
{
	public static class Payment
	{
		public static Error InvalidId => Error.Validation(
			code: "Payment.InvalidId",
			description: "Id оплаты не найден.");

		public static Error NotFound => Error.NotFound(
			code: "Payment.NotFound",
			description: "Оплата не найдена не найден.");

		public static Error CouldNotSave => Error.Conflict(
			code: "Payment.CouldNotSave",
			description: "Оплата не сохранена.");
	}
}
