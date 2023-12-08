using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using System;
using System.IO;
using WHO.BioHub.Migrator;

namespace WHO.BioHub.DAL.Migrator
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static void Main(string[] _)
        {
            Logger serilogLogger = null;

            try
            {
                var configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", true, false)
                   .AddEnvironmentVariables()
                   .Build();

                serilogLogger = ConfigureSerilogLogger();

                string skipConfirm = Environment.GetEnvironmentVariable("SKIP_CONFIRM");
                if (skipConfirm == null)
                {
                    Console.Write($"Confirm to update database \"{configuration.GetConnectionString("SQLServer")}\" [y/N]: ");
                    ConsoleKeyInfo i = Console.ReadKey();
                    Console.WriteLine("");
                    if (i.KeyChar != 'y' && i.KeyChar != 'Y')
                    {
                        serilogLogger.Information("Operation aborted by the user");
                        return;
                    }
                }

                serilogLogger.Information("Applying migration");
                ServiceCollection services = RegisterServices(configuration, serilogLogger);
                _serviceProvider = services.BuildServiceProvider();

                using IServiceScope scope = _serviceProvider.CreateScope();
                scope.ServiceProvider.GetRequiredService<IContextMigrator>()
                       .Migrate();

                serilogLogger.Information("Migrations have been applied successfully");


            }
            catch (Exception ex)
            {
                if (serilogLogger != null)
                    serilogLogger.Fatal(ex, "Process terminated unexpectedly");
                else
                    Console.WriteLine($"Error reading configuration or setting up logger: {ex.Message}");
            }
            finally
            {
                _serviceProvider?.Dispose();

                if (serilogLogger != null)
                {
                    serilogLogger.Information("Finished.");
                    serilogLogger.Dispose();
                }
            }
        }

        private const string LogOutputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}";
        private static Logger ConfigureSerilogLogger()
        {
            var logBuilder = new LoggerConfiguration()
                .WriteTo.Console(
                    restrictedToMinimumLevel: LogEventLevel.Verbose,
                    outputTemplate: LogOutputTemplate);

            var logger = logBuilder.CreateLogger();
            return logger;
        }

        private static ServiceCollection RegisterServices(IConfigurationRoot configuration, Serilog.ILogger logger)
        {
            var services = new ServiceCollection();

            // logging
            services.AddSingleton<ILoggerProvider>(new SerilogLoggerProvider(logger));
            services.AddLogging(lb => lb.AddSerilog(logger));

            // db context
            services.AddScoped<IContextMigrator, ContextMigrator>();
            services.AddDbContext<BioHubDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SQLServer"),
                   serverOptions =>
                   {
                       serverOptions.MigrationsAssembly(typeof(BioHubDbContext).Assembly.FullName);
                       serverOptions.CommandTimeout((int)TimeSpan.FromMinutes(60).TotalSeconds);
                   });
            });

            return services;
        }
    }
}
