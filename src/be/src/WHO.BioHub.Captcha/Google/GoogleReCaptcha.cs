using System.Text.Json;

namespace WHO.BioHub.Captcha.Google
{
    public class GoogleReCaptcha : ICaptcha
    {
        private readonly GoogleConfig _googleConfig;

        public GoogleReCaptcha(GoogleConfig googleConfig)
        {
            _googleConfig = googleConfig;
        }

        public async Task<GoogleResponse> Verify(string recaptchaResponse)
        {
            using var client = new HttpClient();
            var values = new Dictionary<string, string>
                    {
                        { "secret", _googleConfig.SecretKey },
                        { "response", recaptchaResponse }
                    };

            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync(_googleConfig.GoogleVerifyUrl, content);


            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GoogleResponse>(json);
        }
    }
}