using ErrorOr;

namespace DeliveryService.Services.ProductAPI.App.Common.Errors;

public static partial class Errors
{
	public static class Section
	{
		public static Error InvalidId => Error.Validation(
			code: "Section.InvalidId",
			description: "Id секции не найдено.");
	}
}
