using DeliveryService.App.Courier.Queries;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.Contracts.Courier.Get;
using DeliveryService.Contracts.Customer;
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
			.Map(dest => dest.CountOrder, src => src.CountOrder);

		config.NewConfig<(ConfirmOrderCourierRequest request, string courierId), ConfirmOrderCourierCommand>()
			.Map(dest => dest.CourierId, src => src.courierId)
			.Map(dest => dest.OrderId, src => src.request.OrderId);
	}
}
