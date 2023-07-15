using DeliveryService.App.Courier.Queries;
using DeliveryService.Contracts.Courier.Get;
using Mapster;

namespace DeliveryService.API.Common.Mapping;

public class CourierMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CourierDetailsVm, GetCourierDetailsResponse>()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.Password, src => src.Password)
			.Map(dest => dest.LastName, src => src.LastName)
			.Map(dest => dest.FirstName, src => src.FirstName)
			.Map(dest => dest.BirthDay, src => src.BirthDay)
			.Map(dest => dest.City, src => src.City)
			.Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
			.Map(dest => dest.CountOrder, src => src.CountOrder);
	}
}
