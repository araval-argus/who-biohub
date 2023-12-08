using WHO.BioHub.Captcha.Google;

namespace WHO.BioHub.Captcha
{
    public interface ICaptcha
    {
        Task<GoogleResponse> Verify(string recaptchaResponse);
    }
}