using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubEmails;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALWorklistFromBioHubEmailsExtensions
{
    public static IServiceCollection AddDALWorklistFromBioHubEmails(this IServiceCollection services)
    {
        services
            .AddScoped<IWorklistFromBioHubEmailReadRepository, SQLWorklistFromBioHubEmailReadRepository>();

        return services;
    }
}