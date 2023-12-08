using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.CreateSpecimenType;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.DeleteSpecimenType;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ListSpecimenTypes;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.ReadSpecimenType;
using WHO.BioHub.DataManagement.Core.UseCases.SpecimenTypes.UpdateSpecimenType;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionSpecimenTypesExtensions
{
    public static IServiceCollection AddCoreSpecimenTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateSpecimenTypeHandler, CreateSpecimenTypeHandler>()
            .AddScoped<ICreateSpecimenTypeMapper, CreateSpecimenTypeMapper>()
            .AddScoped<CreateSpecimenTypeCommandValidator>()

            .AddScoped<IReadSpecimenTypeHandler, ReadSpecimenTypeHandler>()
            .AddScoped<ReadSpecimenTypeQueryValidator>()

            .AddScoped<IUpdateSpecimenTypeHandler, UpdateSpecimenTypeHandler>()
            .AddScoped<IUpdateSpecimenTypeMapper, UpdateSpecimenTypeMapper>()
            .AddScoped<UpdateSpecimenTypeCommandValidator>()

            .AddScoped<IDeleteSpecimenTypeHandler, DeleteSpecimenTypeHandler>()
            .AddScoped<DeleteSpecimenTypeCommandValidator>()

            .AddScoped<IListSpecimenTypesHandler, ListSpecimenTypesHandler>()
            .AddScoped<ListSpecimenTypesQueryValidator>()
            ;

        return services;
    }
}