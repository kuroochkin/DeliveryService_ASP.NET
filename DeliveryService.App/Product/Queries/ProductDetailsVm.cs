namespace DeliveryService.App.Product.Queries;

public record ProductDetailsVm(
    string ProductId,
    string Title,
    string Price,
    string Thumbnail,
    string Section
	);
