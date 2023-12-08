using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WHO.BioHub.DAL.Extensions;

public static class IServiceCollectionDALExtensions
{
    public static IServiceCollection AddDAL(this IServiceCollection services, string sqlConnectionString)
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