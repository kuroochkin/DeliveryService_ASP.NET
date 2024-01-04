using DeliveryService.Services.CourierAPI.App.Common.Interfaces;
using DeliveryService.Services.CourierAPI.App.Courier.Queries.GetCourierDetails;
using DeliveryService.Services.CourierAPI.Contracts.Courier.Get;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryService.Services.CourierAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/courier")]
public class CourierController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IUserContext _userContext;

    public CourierController(ISender mediator, IMapper mapper, IUserContext userContext)
    {
        _mediator = mediator;
        _mapper = mapper;
        _userContext = userContext;
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetDetailsCourier()
    {
        var courierId = _userContext.CurrentUserId.ToString();

        var query = new GetCourierDetailsQuery(courierId);

        var result = await _mediator.Send(query);

        return result.Match(
            courier => Ok(_mapper.Map<GetCourierDetailsResponse>(courier)),
            errors => Problem("Ошибка")
        );
    }
}
