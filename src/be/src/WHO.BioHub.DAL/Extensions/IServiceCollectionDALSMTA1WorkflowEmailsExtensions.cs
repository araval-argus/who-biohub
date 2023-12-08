using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowEmails;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALSMTA1WorkflowEmailsExtensions
{
    public static IServiceCollection AddDALSMTA1WorkflowEmails(this IServiceCollection services)
    {
        services
            .AddScoped<ISMTA1WorkflowEmailReadRepository, SQLSMTA1WorkflowEmailReadRepository>()
            .AddScoped<ISMTA1WorkflowEmailWriteRepository, SQLSMTA1WorkflowEmailWriteRepository>();

        return services;
    }
}