using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.CreateCourier;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.DeleteCourier;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.ListCouriers;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.ReadCourier;
using WHO.BioHub.DataManagement.Core.UseCases.Couriers.UpdateCourier;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionCouriersExtensions
{
    public static IServiceCollection AddCoreCouriers(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateCourierHandler, CreateCourierHandler>()
            .AddScoped<ICreateCourierMapper, CreateCourierMapper>()
            .AddScoped<CreateCourierCommandValidator>()

            .AddScoped<IReadCourierHandler, ReadCourierHandler>()
            .AddScoped<IReadCourierMapper, ReadCourierMapper>()
            .AddScoped<ReadCourierQueryValidator>()

            .AddScoped<IUpdateCourierHandler, UpdateCourierHandler>()
            .AddScoped<IUpdateCourierMapper, UpdateCourierMapper>()
            .AddScoped<UpdateCourierCommandValidator>()

            .AddScoped<IDeleteCourierHandler, DeleteCourierHandler>()
            .AddScoped<DeleteCourierCommandValidator>()

            .AddScoped<IListCouriersHandler, ListCouriersHandler>()
            .AddScoped <IListCouriersMapper, ListCouriersMapper>()
            .AddScoped<ListCouriersQueryValidator>()
            ;

        return services;
    }
}