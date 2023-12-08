using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;

namespace WHO.BioHub.DataManagement.Core.Extensions;

public static class IAuthChecksExtensions
{
    public static IServiceCollection AddCoreAuthChecks(this IServiceCollection services)
    {
        services

            .AddScoped<IGetAccessInformationHandler, GetAccessInformationHandler>()
            .AddScoped<GetAccessInformationQueryValidator>()

            ;

        return services;
    }
}