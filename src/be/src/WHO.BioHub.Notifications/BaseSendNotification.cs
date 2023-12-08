namespace WHO.BioHub.Notifications
{
    public abstract class BaseSendNotification : ISendNotification
    {
        protected abstract Task SendEmail(MailParams mailParams);

        public async Task SendEmail(IEnumerable<string> tos, IEnumerable<string> ccs, IEnumerable<string> bccs, string body, string from = "", string subject = "")
        {
            var mailParams = new MailParams
            {
                From = from,
                Subject = subject,
                Body = body,
                To = tos == null ? Array.Empty<string>() : tos.ToArray(),
                Cc = ccs == null ? Array.Empty<string>() : ccs.ToArray(),
                Bcc = bccs == null ? Array.Empty<string>() : bccs.ToArray(),
            };

            await SendEmail(mailParams);
        }
    }
}
