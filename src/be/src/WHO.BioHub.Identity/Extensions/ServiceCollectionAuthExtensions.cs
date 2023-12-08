using Microsoft.Extensions.DependencyInjection;

namespace WHO.BioHub.Identity.Extensions
{
    public static class ServiceCollectionAuthExtensions
    {
        public static IServiceCollection AddBioHubAuth(this IServiceCollection services, AzureAd azureAd)
        {
            services
                .AddScoped<IAzureADTokenValidation>(_ => new AzureADTokenValidation(azureAd));

            return services;
        }
    }
}
