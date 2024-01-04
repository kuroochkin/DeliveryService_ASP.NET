namespace DeliveryService.Services.CourierAPI.App.Common.Interfaces;

public interface IUserContext
{
    /// <summary>
    /// ИД текущего пользователя
    /// </summary>
    Guid CurrentUserId { get; }
}
