namespace DeliveryService.AuthAPI.Model.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
