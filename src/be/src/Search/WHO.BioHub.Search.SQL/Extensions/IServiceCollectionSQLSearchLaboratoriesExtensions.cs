using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.Search.SQL.Repositories.Entities;

namespace WHO.BioHub.Search.SQL.Extensions;

public static class IServiceCollectionSQLSearchLaboratoriesExtensions
{
    public static IServiceCollection AddSQLSearchLaboratories(this IServiceCollection services)
    {
        services
            .AddScoped<ILaboratorySearchRepository, SQLLaboratorySearchRepository>();

        return services;
    }
}