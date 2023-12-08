using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.CreateMaterialShippingInformation;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.DeleteMaterialShippingInformation;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ListMaterialShippingInformations;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ReadMaterialShippingInformation;
using WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.UpdateMaterialShippingInformation;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionMaterialShippingInformationsExtensions
{
    public static IServiceCollection AddCoreMaterialShippingInformations(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateMaterialShippingInformationHandler, CreateMaterialShippingInformationHandler>()
            .AddScoped<ICreateMaterialShippingInformationMapper, CreateMaterialShippingInformationMapper>()
            .AddScoped<CreateMaterialShippingInformationCommandValidator>()

            .AddScoped<IReadMaterialShippingInformationHandler, ReadMaterialShippingInformationHandler>()
            .AddScoped<ReadMaterialShippingInformationQueryValidator>()

            .AddScoped<IUpdateMaterialShippingInformationHandler, UpdateMaterialShippingInformationHandler>()
            .AddScoped<IUpdateMaterialShippingInformationMapper, UpdateMaterialShippingInformationMapper>()
            .AddScoped<UpdateMaterialShippingInformationCommandValidator>()

            .AddScoped<IDeleteMaterialShippingInformationHandler, DeleteMaterialShippingInformationHandler>()
            .AddScoped<DeleteMaterialShippingInformationCommandValidator>()

            .AddScoped<IListMaterialShippingInformationsHandler, ListMaterialShippingInformationsHandler>()
            .AddScoped<ListMaterialShippingInformationsQueryValidator>()
            ;

        return services;
    }
}