namespace DeliveryService.AuthAPI.Model.Responses
{
    public class TokenResponse
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; }

        public int ExpiresIn { get; set; }

        public string Username { get; set; }

        public string Role { get; set; }
    }
}
