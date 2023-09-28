using AutoMapper;

namespace DeliveryService.Services.PaymentAPI.Mapping;

public class MappingConfig
{
	public static MapperConfiguration RegisterMaps()
	{
		var mappingConfig = new MapperConfiguration(config =>
		{

		});

		return mappingConfig;
	}
}
