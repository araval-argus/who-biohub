using Microsoft.Extensions.DependencyInjection;

using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListLaboratories;
using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListMapLaboratories;
using WHO.BioHub.PublicData.Core.UseCases.Laboratories.ReadLaboratory;

namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionLaboratoriesExtensions
{
    public static IServiceCollection AddCoreLaboratories(this IServiceCollection services)
    {
        services
            .AddScoped<IReadLaboratoryHandler, ReadLaboratoryHandler>()
            .AddScoped<ReadLaboratoryQueryValidator>()

            .AddScoped<IListLaboratoriesHandler, ListLaboratoriesHandler>()
            .AddScoped<ListLaboratoriesQueryValidator>()
            
            .AddScoped<IListMapLaboratoriesHandler, ListMapLaboratoriesHandler>()
            .AddScoped<IListMapLaboratoryMapper, ListMapLaboratoryMapper>()
            .AddScoped<ListMapLaboratoriesQueryValidator>()

            .AddScoped<IReadLaboratoryMapper, ReadLaboratoryMapper>()
            .AddScoped<IListLaboratoryMapper, ListLaboratoryMapper>()
            ;

        return services;
    }
}