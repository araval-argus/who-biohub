using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Models.Repositories.SpecimenTypes;
using WHO.BioHub.DAL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALSpecimenTypesExtensions
{
    public static IServiceCollection AddDALSpecimenTypes(this IServiceCollection services)
    {
        services
            .AddScoped<ISpecimenTypeReadRepository, SQLSpecimenTypeReadRepository>()
            .AddScoped<ISpecimenTypeWriteRepository, SQLSpecimenTypeWriteRepository>();

        return services;
    }
}