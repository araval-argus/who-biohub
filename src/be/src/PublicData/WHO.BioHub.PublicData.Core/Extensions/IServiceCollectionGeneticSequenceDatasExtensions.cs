using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;
using WHO.BioHub.PublicData.Core.UseCases.GeneticSequenceDatas.ReadGeneticSequenceData;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionGeneticSequenceDatasExtensions
{
    public static IServiceCollection AddCoreGeneticSequenceDatas(this IServiceCollection services)
    {
        services
            .AddScoped<IReadGeneticSequenceDataHandler, ReadGeneticSequenceDataHandler>()
            .AddScoped<ReadGeneticSequenceDataQueryValidator>()

            .AddScoped<IListGeneticSequenceDatasHandler, ListGeneticSequenceDatasHandler>()
            .AddScoped<ListGeneticSequenceDatasQueryValidator>()
            ;

        return services;
    }
}