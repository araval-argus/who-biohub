using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.DocumentTemplates;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALDocumentTemplatesExtensions
{
    public static IServiceCollection AddDALDocumentTemplates(this IServiceCollection services)
    {
        services
            .AddScoped<IDocumentTemplateReadRepository, SQLDocumentTemplateReadRepository>()
            .AddScoped<IDocumentTemplateWriteRepository, SQLDocumentTemplateWriteRepository>();

        return services;
    }
}
