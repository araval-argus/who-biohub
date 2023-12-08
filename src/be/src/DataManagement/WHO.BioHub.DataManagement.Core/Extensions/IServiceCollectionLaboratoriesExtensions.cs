using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratoryFromUserRequest;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.DeleteLaboratory;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListLaboratories;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListMapLaboratories;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadLaboratory;
using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionLaboratoriesExtensions
{
    public static IServiceCollection AddCoreLaboratories(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateLaboratoryHandler, CreateLaboratoryHandler>()
            .AddScoped<ICreateLaboratoryMapper, CreateLaboratoryMapper>()
            .AddScoped<CreateLaboratoryCommandValidator>()

            .AddScoped<IReadLaboratoryHandler, ReadLaboratoryHandler>()
            .AddScoped<IReadLaboratoryMapper, ReadLaboratoryMapper>()            
            .AddScoped<ReadLaboratoryQueryValidator>()

            .AddScoped<IUpdateLaboratoryHandler, UpdateLaboratoryHandler>()
            .AddScoped<IUpdateLaboratoryMapper, UpdateLaboratoryMapper>()
            .AddScoped<UpdateLaboratoryCommandValidator>()

            .AddScoped<IDeleteLaboratoryHandler, DeleteLaboratoryHandler>()
            .AddScoped<DeleteLaboratoryCommandValidator>()

            .AddScoped<IListLaboratoriesHandler, ListLaboratoriesHandler>()
            .AddScoped<IListLaboratoryMapper, ListLaboratoryMapper>()            
            .AddScoped<ListLaboratoriesQueryValidator>()

            .AddScoped<IListMapLaboratoriesHandler, ListMapLaboratoriesHandler>()
            .AddScoped<IListMapLaboratoryMapper, ListMapLaboratoryMapper>()
            .AddScoped<ListMapLaboratoriesQueryValidator>()

            .AddScoped<ICreateLaboratoryFromUserRequestHandler, CreateLaboratoryFromUserRequestHandler>()
            .AddScoped<ICreateLaboratoryFromUserRequestMapper, CreateLaboratoryFromUserRequestMapper>()
            .AddScoped<CreateLaboratoryFromUserRequestCommandValidator>()
            ;

        return services;
    }
}