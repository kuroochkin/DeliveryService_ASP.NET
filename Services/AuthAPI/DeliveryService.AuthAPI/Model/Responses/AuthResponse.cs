using Newtonsoft.Json;

namespace DeliveryService.AuthAPI.Model.Responses
{
    public class AuthResponse
    {
        public bool Success { get; set; }
        public TokenResponse Token { get; set; }
        public string Error { get; set; }
    }
}
