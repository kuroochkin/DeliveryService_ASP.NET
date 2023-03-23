using DeliveryService.App.Courier.Commands.СreateCourier;
using DeliveryService.Contracts.Courier;
using Mapster;

namespace DeliveryService.API.Common.Mapping;

public class CourierMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CreateCourierRequest, СreateCourierCommand>()
			.Map(dest => dest.LastName, src => src.LastName)
			.Map(dest => dest.FirstName, src => src.FirstName)
			.Map(dest => dest.Patromymic, src => src.Patronymic);
	}
}
