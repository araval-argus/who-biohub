using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Graph.Extensions
{
    public static class ServiceCollectionGraphExtensions
    {
        public static IServiceCollection AddBioHubGraph(this IServiceCollection services, GraphAd graphAd, GraphInvitation graphInvitationConfig, ApplicationConfiguration applicationConfiguration)
        {
            services
                .AddScoped<IAzureADUserInvitation, AzureADUserInvitation>()
                .AddScoped<IGraphUtility>(_ => new GraphUtility(applicationConfiguration, graphAd, graphInvitationConfig));

            return services;
        }
    }
}
