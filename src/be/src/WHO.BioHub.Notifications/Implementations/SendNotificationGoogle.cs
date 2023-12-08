using System.Net;
using System.Net.Mail;

namespace WHO.BioHub.Notifications.Implementations
{
    public class SendNotificationGoogle : BaseSendNotification
    {
        private readonly SmtpClient _smtpClient;
        private readonly MailServiceConfig _mailServiceConfig;

        public SendNotificationGoogle(SmtpClientConfig smtpClientConfig, MailServiceConfig mailServiceConfig)
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
            var from = !string.IsNullOrEmpty(mailParams.From) ? mailParams.From : _mailServiceConfig.DefaultFrom;
            var to = string.Join(",", mailParams.To ?? Array.Empty<string>());
            var mailMessage = new MailMessage(from, to);
            foreach (var cc in mailParams.Cc ?? Array.Empty<string>())
            {
                mailMessage.CC.Add(new MailAddress(cc));
            }
            foreach (var bcc in mailParams.Bcc ?? Array.Empty<string>())
            {
                mailMessage.Bcc.Add(new MailAddress(bcc));
            }

            mailMessage.Subject = !string.IsNullOrEmpty(mailParams.Subject) ? mailParams.Subject : _mailServiceConfig.DefaultSubject;
            mailMessage.SubjectEncoding = mailParams.SubjectEncoding ?? _mailServiceConfig.DefaultSubjectEncoding;
            mailMessage.Body = mailParams.Body;
            mailMessage.BodyEncoding = mailParams.BodyEncoding ?? _mailServiceConfig.DefaultBodyEncoding;
            mailMessage.IsBodyHtml = mailParams.IsBodyHtml ?? _mailServiceConfig.DefaultIsBodyHtml;

            await Task.Run(() => _smtpClient.Send(mailMessage));
        }
    }
}