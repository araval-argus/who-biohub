using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALGeneticSequenceDatasExtensions
{
    public static IServiceCollection AddPublicDALGeneticSequenceDatas(this IServiceCollection services)
    {
        services
            .AddScoped<IGeneticSequenceDataPublicReadRepository, SQLGeneticSequenceDataPublicReadRepository>();

        return services;
    }
}