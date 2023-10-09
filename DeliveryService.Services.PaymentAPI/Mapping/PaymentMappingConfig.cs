using DeliveryService.Services.PaymentAPI.App.Payment.Commands.ChangePayment;
using DeliveryService.Services.PaymentAPI.Contracts.Payment.Requests;
using Mapster;

namespace DeliveryService.Services.PaymentAPI.Mapping;

public class PaymentMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<ChangePaymentStatusRequest, ChangePaymentStatusCommand>()
			.Map(dest => dest.OrderId, src => src.OrderId);
	}
}
