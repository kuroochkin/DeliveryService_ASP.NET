namespace DeliveryService.Services.ProductAPI.App.Vm.Product;

public record ProductDetailsVm(
	string ProductId,
	string Title,
	string Price,
	string Thumbnail,
	string? Section
	);
