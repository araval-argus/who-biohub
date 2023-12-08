namespace WHO.BioHub.Notifications.Implementations
{
    public class SendNotificationFake : BaseSendNotification
    {
        protected override async Task SendEmail(MailParams mailParams)
        {
            await Task.CompletedTask;
        }
    }
}
