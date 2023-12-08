using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALLaboratoriesExtensions
{
    public static IServiceCollection AddPublicDALLaboratories(this IServiceCollection services)
    {
        services
            .AddScoped<ILaboratoryPublicReadRepository, SQLLaboratoryPublicReadRepository>();

        return services;
    }
}