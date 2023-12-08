using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Public.SQL.Repositories;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALExtensions
{
    public static IServiceCollection AddPublicDAL(this IServiceCollection services, string sqlConnectionString)
    {
#if DEBUG
        services.AddDbContext<BioHubDbContext>(options
            => options.UseSqlServer(sqlConnectionString,
            serverOptions =>
            {
                serverOptions.MigrationsAssembly(typeof(BioHubDbContext).Assembly.FullName);
                serverOptions.CommandTimeout((int)TimeSpan.FromMinutes(60).TotalSeconds);
            }));

#else
        services.AddDbContextFactory<BioHubDbContext>(options
            => options.UseSqlServer(sqlConnectionString));
#endif
        return services;
    }
}