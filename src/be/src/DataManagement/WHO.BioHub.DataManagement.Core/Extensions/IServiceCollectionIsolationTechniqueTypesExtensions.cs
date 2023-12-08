using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.CreateIsolationTechniqueType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.DeleteIsolationTechniqueType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ReadIsolationTechniqueType;
using WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.UpdateIsolationTechniqueType;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionIsolationTechniqueTypesExtensions
{
    public static IServiceCollection AddCoreIsolationTechniqueTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateIsolationTechniqueTypeHandler, CreateIsolationTechniqueTypeHandler>()
            .AddScoped<ICreateIsolationTechniqueTypeMapper, CreateIsolationTechniqueTypeMapper>()
            .AddScoped<CreateIsolationTechniqueTypeCommandValidator>()

            .AddScoped<IReadIsolationTechniqueTypeHandler, ReadIsolationTechniqueTypeHandler>()
            .AddScoped<ReadIsolationTechniqueTypeQueryValidator>()

            .AddScoped<IUpdateIsolationTechniqueTypeHandler, UpdateIsolationTechniqueTypeHandler>()
            .AddScoped<IUpdateIsolationTechniqueTypeMapper, UpdateIsolationTechniqueTypeMapper>()
            .AddScoped<UpdateIsolationTechniqueTypeCommandValidator>()

            .AddScoped<IDeleteIsolationTechniqueTypeHandler, DeleteIsolationTechniqueTypeHandler>()
            .AddScoped<DeleteIsolationTechniqueTypeCommandValidator>()

            .AddScoped<IListIsolationTechniqueTypesHandler, ListIsolationTechniqueTypesHandler>()
            .AddScoped<ListIsolationTechniqueTypesQueryValidator>()
            ;

        return services;
    }
}