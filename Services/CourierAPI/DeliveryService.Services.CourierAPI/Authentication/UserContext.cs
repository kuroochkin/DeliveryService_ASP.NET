using System.Security.Claims;
using DeliveryService.Services.CourierAPI.App.Common.Interfaces;

namespace DeliveryService.Services.CourierAPI.Authentication;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="httpContextAccessor">Адаптер Http-context'а</param>
    public UserContext(IHttpContextAccessor httpContextAccessor)
        => _httpContextAccessor = httpContextAccessor;

    /// <inheritdoc/>
    public Guid CurrentUserId => Guid.TryParse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId) ? userId : Guid.Empty;

    private ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
}