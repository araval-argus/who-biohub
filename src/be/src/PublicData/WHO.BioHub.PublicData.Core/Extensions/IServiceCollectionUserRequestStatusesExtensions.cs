using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.PublicData.Core.UseCases.UserRequestStatuses.ReadUserRequestStatus;



namespace WHO.BioHub.PublicData.Core.Extensions;

public static class IServiceCollectionUserRequestStatusesExtensions
{
    public static IServiceCollection AddCoreUserRequestStatuses(this IServiceCollection services)
    {
        services
            .AddScoped<IReadUserRequestStatusHandler, ReadUserRequestStatusHandler>()
            .AddScoped<ReadUserRequestStatusQueryValidator>()
            .AddScoped<IReadUserRequestStatusByStatusHandler, ReadUserRequestStatusByStatusHandler>()
            .AddScoped<ReadUserRequestStatusByStatusQueryValidator>();

        return services;
    }
}