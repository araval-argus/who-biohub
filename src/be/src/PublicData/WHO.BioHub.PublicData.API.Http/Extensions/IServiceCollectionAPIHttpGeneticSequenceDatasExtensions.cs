using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.API.Http.Controllers;

namespace WHO.BioHub.PublicData.API.Http.Extensions;

public static class IServiceCollectionAPIHttpGeneticSequenceDatasExtensions
{
    public static IServiceCollection AddAPIHttpGeneticSequenceDatas(this IServiceCollection services)
    {
        services
            .AddScoped<IGeneticSequenceDatasController, GeneticSequenceDatasController>();

        return services;
    }
}