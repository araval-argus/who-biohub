using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WHO.BioHub.DAL;

namespace WHO.BioHub.Migrator
{
    /// <summary>
    /// This class can be used to migrate the database, however it is preferred to use
    /// `services.EnsureMigrationOfContext<DB_CONTEXT>()` in Startup after services' registration
    /// </summary>
    public interface IContextMigrator
    {
        void Migrate();
    }

    public class ContextMigrator : IContextMigrator
    {
        private readonly ILogger<ContextMigrator> _logger;
        private readonly BioHubDbContext _dbContext;

        public ContextMigrator(
            ILogger<ContextMigrator> logger,
            BioHubDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public void Migrate()
        {
            try
            {
                _logger.LogInformation("Ensuring db is up to date");
                var pendingMigrations = _dbContext.Database.GetPendingMigrations();
                if (!pendingMigrations.Any())
                {
                    _logger.LogInformation("No need to update database: no migrations pending");
                    return;
                }

                int migrationCount = pendingMigrations.Count();
                _logger.LogInformation("Database update needed: {migrationCount} migration(s) need to be applied", migrationCount);
                _dbContext.Database.Migrate();
                _logger.LogInformation("Database updated successfully: {migrationCount} migration(s) applied", migrationCount);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error updating the database");
                throw;
            }
        }
    }
}
