using DeliveryService.Services.ProductAPI.App.Vm.Section;
using DeliveryService.Services.ProductAPI.Contracts.Section.Get;
using Mapster;

namespace DeliveryService.Services.ProductAPI.Mapping;

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
