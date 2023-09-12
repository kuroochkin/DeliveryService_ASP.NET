using DeliveryService.App.Customer.Commands.EditProfile;
using DeliveryService.App.Customer.Queries;
using DeliveryService.App.Order.Commands.ConfirmOrder;
using DeliveryService.Contracts.Customer;
using DeliveryService.Contracts.Customer.Get;
using DeliveryService.Contracts.Order;
using Mapster;

namespace DeliveryService.API.Common.Mapping;

public class CustomerMappingConfig : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CustomerDetailsVm, GetCustomerDetailsResponse>()
			.Map(dest => dest.Id, src => src.Id)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.Password, src => src.Password)
			.Map(dest => dest.LastName, src => src.LastName)
			.Map(dest => dest.FirstName, src => src.FirstName)
			.Map(dest => dest.BirthDay, src => src.BirthDay)
			.Map(dest => dest.CountOrder, src => src.CountOrder);

		config.NewConfig<EditCustomerProfileRequest, EditProfileCommand>()
			.Map(dest => dest.CustomerId, src => src.CustomerId)
			.Map(dest => dest.Email, src => src.Email)
			.Map(dest => dest.Password, src => src.Password)
			.Map(dest => dest.LastName, src => src.LastName)
			.Map(dest => dest.FirstName, src => src.FirstName)
			.Map(dest => dest.Birthday, src => src.Birthday)
			.Map(dest => dest.CountOrder, src => src.CountOrder);

	}
}
