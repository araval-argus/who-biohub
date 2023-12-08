using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.DAL.Repositories;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALUserRequestsExtensions
{
    public static IServiceCollection AddPublicDALUserRequests(this IServiceCollection services)
    {
        services
            .AddScoped<IUserRequestPublicReadRepository, SQLUserRequestPublicReadRepository>()
         .AddScoped<IUserRequestPublicWriteRepository, SQLUserRequestPublicWriteRepository>();

        return services;
    }
}