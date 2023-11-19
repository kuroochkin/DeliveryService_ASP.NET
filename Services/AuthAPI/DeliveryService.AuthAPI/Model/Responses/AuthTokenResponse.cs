namespace DeliveryService.AuthAPI.Model.Responses
{
    /// <summary>
    /// Модель ответа токена для своего сервиса авторизации
    /// </summary>
    public class AuthTokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
