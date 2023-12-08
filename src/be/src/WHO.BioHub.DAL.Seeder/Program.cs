using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using WHO.BioHub.DAL.Seeder.Data;

namespace WHO.BioHub.DAL.Seeder;

class Program
{
    static async Task Main(string[] _)
    {
        try
        {
            // load configuration
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", true, false)
               .AddEnvironmentVariables()
               .Build();

            // ask confirmation
            string? skipConfirm = Environment.GetEnvironmentVariable("SKIP_CONFIRM");
            if (skipConfirm == null)
            {
                Console.Write($"Confirm to seed database \"{configuration.GetConnectionString("SQLServer")}\" [y/N]: ");
                ConsoleKeyInfo i = Console.ReadKey();
                Console.WriteLine("");
                if (i.KeyChar != 'y' && i.KeyChar != 'Y')
                {
                    Console.WriteLine("Operation aborted by the user");
                    return;
                }
            }

            // configure logger
            Log.Logger = ConfigureSerilogLogger();

            // build host and run
            using IHost host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<SeedDataService>();
                    RegisterServices(services, configuration);
                })
                .Build();
            await host.StartAsync(default);
        }
        catch (Exception ex)
        {
            string errorMessage = "Process terminated unexpectedly";
            if (Log.Logger != null)
            {
                Log.Logger.Fatal(ex, errorMessage);
            }
            else
            {
                Console.WriteLine(errorMessage);
                Console.WriteLine(ex.StackTrace);
            };
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

    private static void RegisterServices(IServiceCollection services, IConfigurationRoot configuration)
    {
        // logging
        services.AddSingleton<ILoggerProvider>(new SerilogLoggerProvider(Log.Logger));
        services.AddLogging(lb => lb.AddSerilog(Log.Logger));

        // db context            
        services.AddDbContext<BioHubDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SQLServer"),
               serverOptions =>
               {
                   serverOptions.MigrationsAssembly(typeof(BioHubDbContext).Assembly.FullName);
                   serverOptions.CommandTimeout((int)TimeSpan.FromMinutes(60).TotalSeconds);
               });
        });

        // seed data service
        services
            .AddScoped<ISeedData, SeedData>()
            .AddScoped<SeedDataService>();
    }
}
