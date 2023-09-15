using DeliveryService.App.Customer.Commands.EditProfile;
using DeliveryService.App.Customer.Queries.GetCustomerDetails;
using DeliveryService.Contracts.Customer;
using DeliveryService.Contracts.Customer.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.API.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ApiController
{
	private readonly ISender _mediator;
	private readonly IMapper _mapper;

	public CustomerController(ISender mediator, IMapper mapper)
	{
		_mediator = mediator;
		_mapper = mapper;
	}

	/// <summary>
	/// Получение информации о конкретном заказчике
	/// </summary>
	/// <returns></returns>
	[HttpGet("profile")]
	public async Task<IActionResult> GetDetailsCustomer()
	{
		var customerId = GetUserId();

		var query = new GetCustomerDetailsQuery(customerId);

		var result = await _mediator.Send(query);

		return result.Match(
			customer => Ok(_mapper.Map<GetCustomerDetailsResponse>(customer)),
			errors => Problem("Ошибка")
		);
	}

	/// <summary>
	/// Редактирование профиля заказчика
	/// </summary>
	/// <param name="request"></param>
	/// <returns></returns>
	[HttpPost("editProfile")]
	[Authorize(Roles = "Customer")]
	public async Task<IActionResult> EditProfile(EditCustomerProfileRequest request)
	{
		var customer = GetUserId();

		var command = _mapper.Map<EditProfileCommand>((request, customer));

		var result = await _mediator.Send(command);

		return result.Match(
			Result => Ok(result.Value),
			errors => Problem("Ошибка")
			);
	}
}
