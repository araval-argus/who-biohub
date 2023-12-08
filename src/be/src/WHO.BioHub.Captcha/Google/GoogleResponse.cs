using System.Text.Json;
using System.Text.Json.Serialization;

namespace WHO.BioHub.Captcha.Google
{
    public class GoogleResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("challenge_ts")]
        public string? ChallengeTs { get; set; }

        [JsonPropertyName("hostname")]
        public string? Hostname { get; set; }

        [JsonExtensionData]
        public IDictionary<string, JsonElement>? ExtensionData { get; set; }
    }
}