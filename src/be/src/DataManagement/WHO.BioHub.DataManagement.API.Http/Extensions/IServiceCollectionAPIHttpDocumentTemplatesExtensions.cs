using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.API.Http.Controllers;

namespace WHO.BioHub.DataManagement.API.Http.Extensions;

public static class IServiceCollectionAPIHttpDocumentTemplatesExtensions
{
    public static IServiceCollection AddAPIHttpDocumentTemplates(this IServiceCollection services)
    {
        services
            .AddScoped<IDocumentTemplatesController, DocumentTemplatesController>();

        return services;
    }
}