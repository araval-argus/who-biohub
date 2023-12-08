using System.Net;
using System.Net.Mail;

namespace WHO.BioHub.Notifications.Implementations
{
    public class SendNotificationSendGrid : BaseSendNotification
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailServiceConfig _mailServiceConfig;

        public SendNotificationSendGrid(SmtpClientConfig smtpClientConfig, MailServiceConfig mailServiceConfig)
        {
            _smtpClient = new SmtpClient(smtpClientConfig.Smtp, smtpClientConfig.Port)
            {
                Credentials = new NetworkCredential(smtpClientConfig.Username, smtpClientConfig.Password),
                EnableSsl = smtpClientConfig.EnableSsl
            };
            _mailServiceConfig = mailServiceConfig;
        }

        protected override async Task SendEmail(MailParams mailParams)
        {
            throw new NotImplementedException();
        }
    }
}
