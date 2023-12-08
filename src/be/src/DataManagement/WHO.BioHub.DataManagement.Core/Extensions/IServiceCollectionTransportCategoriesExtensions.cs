using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.CreateTransportCategory;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.DeleteTransportCategory;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ListTransportCategories;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.ReadTransportCategory;
using WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.UpdateTransportCategory;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionTransportCategoriesExtensions
{
    public static IServiceCollection AddCoreTransportCategories(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateTransportCategoryHandler, CreateTransportCategoryHandler>()
            .AddScoped<ICreateTransportCategoryMapper, CreateTransportCategoryMapper>()
            .AddScoped<CreateTransportCategoryCommandValidator>()

            .AddScoped<IReadTransportCategoryHandler, ReadTransportCategoryHandler>()
            .AddScoped<ReadTransportCategoryQueryValidator>()

            .AddScoped<IUpdateTransportCategoryHandler, UpdateTransportCategoryHandler>()
            .AddScoped<IUpdateTransportCategoryMapper, UpdateTransportCategoryMapper>()
            .AddScoped<UpdateTransportCategoryCommandValidator>()

            .AddScoped<IDeleteTransportCategoryHandler, DeleteTransportCategoryHandler>()
            .AddScoped<DeleteTransportCategoryCommandValidator>()

            .AddScoped<IListTransportCategoriesHandler, ListTransportCategoriesHandler>()
            .AddScoped<ListTransportCategoriesQueryValidator>()
            ;

        return services;
    }
}