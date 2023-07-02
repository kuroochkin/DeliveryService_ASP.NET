using DeliveryService.App.Section.Queries;
using DeliveryService.Contracts.Section.Get;
using Mapster;

namespace DeliveryService.API.Common.Mapping;

public class SectionMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<SectionVm, GetSectionResponse>()
			.Map(dest => dest.SectionId, src => src.SectionId)
			.Map(dest => dest.Name, src => src.Name);

		config.NewConfig<SectionsVm, GetAllSectionsResponse>()
			.Map(dest => dest.Sections, src => src.Sections);
	}
}
