using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Documents;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALDocumentsExtensions
{
    public static IServiceCollection AddDALDocuments(this IServiceCollection services)
    {
        services
            .AddScoped<IDocumentReadRepository, SQLDocumentReadRepository>()
            .AddScoped<IDocumentWriteRepository, SQLDocumentWriteRepository>();

        return services;
    }
}