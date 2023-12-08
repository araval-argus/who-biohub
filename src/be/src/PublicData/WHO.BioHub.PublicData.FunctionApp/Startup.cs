using System.Reflection;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Logging;
using Newtonsoft.Json.Serialization;
using Serilog;
using WHO.BioHub.Captcha.Extensions;
using WHO.BioHub.Captcha.Google;
using WHO.BioHub.DAL.Extensions;
using WHO.BioHub.Notifications;
using WHO.BioHub.Notifications.Extensions;
using WHO.BioHub.PublicData.DependencyInjection;
using WHO.BioHub.StorageAccount;
using WHO.BioHub.StorageAccount.Extensions;

[assembly: FunctionsStartup(typeof(WHO.BioHub.PublicData.FunctionApp.Startup))]
namespace WHO.BioHub.PublicData.FunctionApp
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
                .AddPublicDAL(configuration.GetConnectionString("SQLServer"));

            services
                .AddBioHubNotifications(configuration.GetSection("SmtpClientConfig").Get<SmtpClientConfig>(), configuration.GetSection("MailServiceConfig").Get<MailServiceConfig>());

            services
                .AddBioHubCaptcha(configuration.GetSection("GoogleConfig").Get<GoogleConfig>());

            services
               .AddBioHubStorageAccount(configuration.GetSection("StorageAccount").Get<StorageConfiguration>());



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
