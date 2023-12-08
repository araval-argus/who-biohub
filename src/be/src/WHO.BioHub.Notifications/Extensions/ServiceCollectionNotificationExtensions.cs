using Microsoft.Extensions.DependencyInjection;
using WHO.BioHub.Notifications.Implementations;

namespace WHO.BioHub.Notifications.Extensions
{
    public static class ServiceCollectionNotificationExtensions
    {
        public static IServiceCollection AddBioHubNotifications(this IServiceCollection services, SmtpClientConfig smtpClientConfig, MailServiceConfig mailServiceConfig)
        {
            if (Enum.TryParse(mailServiceConfig.DefaultProvider.Trim().ToUpper(), out EmailProviderEnum choosedProvider))
            {
                mailServiceConfig.EmailProviderEnum = choosedProvider;

                switch (mailServiceConfig.EmailProviderEnum)
                {
                    case EmailProviderEnum.FAKE:
                        services.AddScoped<ISendNotification>(_ => new SendNotificationFake());
                        break;
                    case EmailProviderEnum.GOOGLE:
                        services.AddScoped<ISendNotification>(_ => new SendNotificationGoogle(smtpClientConfig, mailServiceConfig));
                        break;
                    case EmailProviderEnum.SEND_GRID:
                        services.AddScoped<ISendNotification>(_ => new SendNotificationSendGrid(smtpClientConfig, mailServiceConfig));
                        break;
                    default:
                        break;
                }
            }
            else
                throw new ArgumentNullException($"No provider defined for {mailServiceConfig.EmailProviderEnum}");

            return services;
        }
    }
}
