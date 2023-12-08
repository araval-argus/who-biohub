using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpDocumentsExtensions
{
    public static IServiceCollection AddAPIHttpDocuments(this IServiceCollection services)
    {
        services
            .AddScoped<IDocumentsController, DocumentsController>();

        return services;
    }
}