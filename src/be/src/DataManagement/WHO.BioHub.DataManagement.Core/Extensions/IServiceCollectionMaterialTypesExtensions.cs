using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.DeleteMaterialType;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ListMaterialTypes;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ReadMaterialType;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.UpdateMaterialType;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialTypesExtensions
{
    public static IServiceCollection AddCoreMaterialTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialTypeHandler, CreateMaterialTypeHandler>()
            .AddScoped<ICreateMaterialTypeMapper, CreateMaterialTypeMapper>()
            .AddScoped<CreateMaterialTypeCommandValidator>()

            .AddScoped<IReadMaterialTypeHandler, ReadMaterialTypeHandler>()
            .AddScoped<ReadMaterialTypeQueryValidator>()

            .AddScoped<IUpdateMaterialTypeHandler, UpdateMaterialTypeHandler>()
            .AddScoped<IUpdateMaterialTypeMapper, UpdateMaterialTypeMapper>()
            .AddScoped<UpdateMaterialTypeCommandValidator>()

            .AddScoped<IDeleteMaterialTypeHandler, DeleteMaterialTypeHandler>()
            .AddScoped<DeleteMaterialTypeCommandValidator>()

            .AddScoped<IListMaterialTypesHandler, ListMaterialTypesHandler>()
            .AddScoped<ListMaterialTypesQueryValidator>()
            ;

        return services;
    }
}