using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Captcha.Google;

namespace WHO.BioHub.Captcha.Extensions
{
    public static class ServiceCollectionCaptchaExtensions
    {
        public static IServiceCollection AddBioHubCaptcha(this IServiceCollection services, GoogleConfig googleConfig)
        {
            services
                .AddScoped<ICaptcha>(_ => new GoogleReCaptcha(googleConfig));

            return services;
        }
    }
}
