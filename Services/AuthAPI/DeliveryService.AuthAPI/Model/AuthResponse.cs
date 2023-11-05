using Newtonsoft.Json;

namespace DeliveryService.AuthAPI.Model
{
    public class AuthResponse
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
