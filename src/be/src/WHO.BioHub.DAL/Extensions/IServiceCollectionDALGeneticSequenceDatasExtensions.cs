using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALGeneticSequenceDatasExtensions
{
    public static IServiceCollection AddDALGeneticSequenceDatas(this IServiceCollection services)
    {
        services
            .AddScoped<IGeneticSequenceDataReadRepository, SQLGeneticSequenceDataReadRepository>()
            .AddScoped<IGeneticSequenceDataWriteRepository, SQLGeneticSequenceDataWriteRepository>();

        return services;
    }
}