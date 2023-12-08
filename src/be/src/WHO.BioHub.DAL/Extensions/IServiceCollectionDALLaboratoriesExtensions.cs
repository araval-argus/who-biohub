using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.Laboratories;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALLaboratoriesExtensions
{
    public static IServiceCollection AddDALLaboratories(this IServiceCollection services)
    {
        services
            .AddScoped<ILaboratoryReadRepository, SQLLaboratoryReadRepository>()
            .AddScoped<ILaboratoryWriteRepository, SQLLaboratoryWriteRepository>();

        return services;
    }
}