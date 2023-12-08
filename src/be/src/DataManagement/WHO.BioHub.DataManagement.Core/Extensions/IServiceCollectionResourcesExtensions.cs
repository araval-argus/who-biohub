using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateFolder;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateResource;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResource;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.DeleteResourceFileToken;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.ListResources;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.ReadResourceFileToken;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.UpdateResource;
using WHO.BioHub.DataManagement.Core.UseCases.Resources.UploadResourceFileToken;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IServiceCollectionResourcesExtensions
{
    public static IServiceCollection AddCoreResources(this IServiceCollection services)
    {
        services
            .AddScoped<ICreateResourceHandler, CreateResourceHandler>()
            .AddScoped<ICreateResourceMapper, CreateResourceMapper>()
            .AddScoped<CreateResourceCommandValidator>()

            .AddScoped<ICreateFolderHandler, CreateFolderHandler>()
            .AddScoped<ICreateFolderMapper, CreateFolderMapper>()
            .AddScoped<CreateFolderCommandValidator>()

            .AddScoped<IReadResourceFileTokenHandler, ReadResourceFileTokenHandler>()
            .AddScoped<ReadResourceFileTokenQueryValidator>()

            .AddScoped<IUploadResourceFileTokenHandler, UploadResourceFileTokenHandler>()
            .AddScoped<UploadResourceFileTokenQueryValidator>()

            .AddScoped<IDeleteResourceFileTokenHandler, DeleteResourceFileTokenHandler>()
            .AddScoped<DeleteResourceFileTokenQueryValidator>()

            .AddScoped<IUpdateResourceHandler, UpdateResourceHandler>()
            .AddScoped<IUpdateResourceMapper, UpdateResourceMapper>()
            .AddScoped<UpdateResourceCommandValidator>()

            .AddScoped<IDeleteResourceHandler, DeleteResourceHandler>()
            .AddScoped<DeleteResourceCommandValidator>()

            .AddScoped<IListResourcesHandler, ListResourcesHandler>()
            .AddScoped<ListResourcesQueryValidator>()
            ;

        return services;
    }
}