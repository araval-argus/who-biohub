using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALWorklistToBioHubEmailsExtensions
{
    public static IServiceCollection AddDALWorklistToBioHubEmails(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistToBioHubEmailReadRepository, SQLWorklistToBioHubEmailReadRepository>()
            .AddScoped<IWorklistToBioHubEmailWriteRepository, SQLWorklistToBioHubEmailWriteRepository>();

        return services;
    }
}
