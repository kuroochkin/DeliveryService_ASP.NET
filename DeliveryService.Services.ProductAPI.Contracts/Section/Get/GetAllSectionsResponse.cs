namespace DeliveryService.Services.ProductAPI.Contracts.Section.Get;

public record GetAllSectionsResponse(
	List<GetSectionResponse> Sections);
