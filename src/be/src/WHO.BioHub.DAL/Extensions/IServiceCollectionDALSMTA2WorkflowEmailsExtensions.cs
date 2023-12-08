using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowEmails;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALSMTA2WorkflowEmailsExtensions
{
    public static IServiceCollection AddDALSMTA2WorkflowEmails(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA2WorkflowEmailReadRepository, SQLSMTA2WorkflowEmailReadRepository>()
            .AddScoped<ISMTA2WorkflowEmailWriteRepository, SQLSMTA2WorkflowEmailWriteRepository>();

        return services;
    }
}