namespace DeliveryService.Contracts.Section.Get;

public record GetAllSectionsResponse(
	List<GetSectionResponse> Sections);
