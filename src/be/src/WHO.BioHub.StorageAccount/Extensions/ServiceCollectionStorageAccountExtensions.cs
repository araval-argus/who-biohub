using Microsoft.Extensions.DependencyInjection;


namespace WHO.BioHub.StorageAccount.Extensions
{
    public static class ServiceCollectionStorageAccountExtensions
    {
        public static IServiceCollection AddBioHubStorageAccount(this IServiceCollection services, StorageConfiguration storageConfiguration)
        {
            services
                .AddScoped<IStorageAccountUtility>(_ => new StorageAccountUtility(storageConfiguration));

            return services;
        }
    }
}
