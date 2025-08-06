using System.Text.Json.Serialization;

namespace WebApplicationElevador.Models.DTOs
{
    public class TokenDataDTO
    {
        [JsonPropertyName("token")]
        public required string Token { get; set; }
        [JsonPropertyName("expiration")]
        public DateTime Expiration { get; set; }
    }
}
