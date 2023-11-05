using Newtonsoft.Json;

namespace DeliveryService.AuthAPI.Model
{
    public class Token
    {
        [JsonProperty("AccessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("TokenType")]
        public string TokenType { get; set; }

        [JsonProperty("ExpiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string Username { get; set; }

        [JsonProperty(".issued")]
        public string IssuedAt { get; set; }

        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }
    }
}
