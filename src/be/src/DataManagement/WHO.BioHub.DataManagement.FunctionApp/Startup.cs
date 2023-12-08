using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using WHO.BioHub.Graph;
using WHO.BioHub.Graph.Extensions;
using WHO.BioHub.Identity;
using WHO.BioHub.Identity.Extensions;
using WHO.BioHub.DAL.Extensions;
using WHO.BioHub.DataManagement.DependencyInjection;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.StorageAccount.Extensions;
using WHO.BioHub.WorkflowEngine.Extensions;
using WHO.BioHub.Notifications;
using WHO.BioHub.Captcha.Google;
using WHO.BioHub.Captcha.Extensions;
using WHO.BioHub.Notifications.Extensions;
using WHO.BioHub.WorkflowEngine;
using WHO.BioHub.Shared.Utils;

[assembly: FunctionsStartup(typeof(WHO.BioHub.DataManagement.FunctionApp.Startup))]
namespace WHO.BioHub.DataManagement.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
#if DEBUG
            IdentityModelEventSource.ShowPII = true;
#endif

            IConfiguration configuration =
                GetConfiguration(builder.GetContext().Configuration);

            AddServices(configuration, builder);
        }

        private static void AddServices(IConfiguration configuration, IFunctionsHostBuilder builder)
        {
            IServiceCollection services = builder.Services;

            // Add Application Insights
            services.AddApplicationInsightsTelemetry();

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

            services
                .AddBioHubAuth(configuration.GetSection("AzureAd").Get<AzureAd>());

            services
                .AddBioHubGraph(configuration.GetSection("GraphAd").Get<GraphAd>(), configuration.GetSection("GraphInvitation").Get<GraphInvitation>(), configuration.GetSection("Application").Get<ApplicationConfiguration>());

            services
                .AddBioHubNotifications(configuration.GetSection("SmtpClientConfig").Get<SmtpClientConfig>(), configuration.GetSection("MailServiceConfig").Get<MailServiceConfig>());

            services
                .AddBioHubCaptcha(configuration.GetSection("GoogleConfig").Get<GoogleConfig>());

            services
               .AddBioHubStorageAccount(configuration.GetSection("StorageAccount").Get<StorageConfiguration>());

            services
               .AddBioHubWorkflowEngine(configuration.GetSection("Application").Get<ApplicationConfiguration>());


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
