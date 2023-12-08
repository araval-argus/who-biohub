using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.CreateGeneticSequenceData;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.DeleteGeneticSequenceData;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ListGeneticSequenceDatas;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.ReadGeneticSequenceData;
using WHO.BioHub.DataManagement.Core.UseCases.GeneticSequenceDatas.UpdateGeneticSequenceData;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionGeneticSequenceDatasExtensions
{
    public static IServiceCollection AddCoreGeneticSequenceDatas(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateGeneticSequenceDataHandler, CreateGeneticSequenceDataHandler>()
            .AddScoped<ICreateGeneticSequenceDataMapper, CreateGeneticSequenceDataMapper>()
            .AddScoped<CreateGeneticSequenceDataCommandValidator>()

            .AddScoped<IReadGeneticSequenceDataHandler, ReadGeneticSequenceDataHandler>()
            .AddScoped<ReadGeneticSequenceDataQueryValidator>()

            .AddScoped<IUpdateGeneticSequenceDataHandler, UpdateGeneticSequenceDataHandler>()
            .AddScoped<IUpdateGeneticSequenceDataMapper, UpdateGeneticSequenceDataMapper>()
            .AddScoped<UpdateGeneticSequenceDataCommandValidator>()

            .AddScoped<IDeleteGeneticSequenceDataHandler, DeleteGeneticSequenceDataHandler>()
            .AddScoped<DeleteGeneticSequenceDataCommandValidator>()

            .AddScoped<IListGeneticSequenceDatasHandler, ListGeneticSequenceDatasHandler>()
            .AddScoped<ListGeneticSequenceDatasQueryValidator>()
            ;

        return services;
    }
}