using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using WHO.BioHub.DAL;

namespace WHO.BioHub.Migrator
{
    public class BioHubDbContextFactory : IDesignTimeDbContextFactory<BioHubDbContext>
    {
        public BioHubDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json")
                               .Build();

            var connStr = config.GetConnectionString("SQLServer");
            var builder = new DbContextOptionsBuilder<BioHubDbContext>();
            builder.UseSqlServer(connStr,
                serverOptions =>
                {
                    serverOptions.MigrationsAssembly(typeof(BioHubDbContext).Assembly.FullName);
                    serverOptions.CommandTimeout((int)TimeSpan.FromMinutes(60).TotalSeconds);
                });
            return new BioHubDbContext(builder.Options);
        }
    }
}
