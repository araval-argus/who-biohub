using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL.Seeder.Data;

namespace WHO.BioHub.DAL.Seeder;

internal class SeedDataService : IHostedService
{
    private readonly ISeedData _seedData;
    private readonly ILogger<SeedDataService> _logger;

    public SeedDataService(
        ILogger<SeedDataService> logger,
        ISeedData seedData)
    {
        _logger = logger;
        _seedData = seedData;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Seeding the database");
        await _seedData.SeedAll(cancellationToken);
        _logger.LogInformation("Database seeded");
    }

    public Task StopAsync(CancellationToken _)
    {
        _logger.LogInformation("Database seed stopped");
        return Task.CompletedTask;
    }
}