using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using WHO.BioHub.DAL.Extensions;
using WHO.BioHub.DocumentManagement.DependencyInjection;
using WHO.BioHub.DocumentManagement.FunctionApp;

[assembly: FunctionsStartup(typeof(Startup))]
namespace WHO.BioHub.DocumentManagement.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
#if DEBUG
            IdentityModelEventSource.ShowPII = true;
#endif

            AddServices(builder);
        }

        private static void AddServices(IFunctionsHostBuilder builder)
        {
            IConfiguration configuration =
                GetConfiguration(builder.GetContext().Configuration);
            IServiceCollection services = builder.Services;

            // Customize Json Serializer
            services
                .AddMvcCore()
                .AddNewtonsoftJson(o => o.SerializerSettings.ContractResolver = new DefaultContractResolver());

            // Add Logging
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(
                    Serilog.Events.LogEventLevel.Verbose,
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} <s:{SourceContext}>{NewLine}{Exception}")
                .CreateLogger();

            services.AddLogging(lb => lb.AddSerilog(Log.Logger, true));

            // Add Main services
            services
                .AddDAL(configuration.GetConnectionString("SQLServer"));

            InjectorExecutor.Inject(services);
        }

        private static IConfiguration GetConfiguration(IConfiguration configuration)
        {
            IConfiguration _configuration =
                new ConfigurationBuilder()
                    .AddConfiguration(configuration)
                    .AddEnvironmentVariables()
#if DEBUG
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
#endif
                    .Build();

            return _configuration;
        }
    }
}
