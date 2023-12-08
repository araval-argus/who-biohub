namespace WHO.BioHub.Notifications
{
    public interface ISendNotification
    {
        Task SendEmail(IEnumerable<string> tos, IEnumerable<string> ccs, IEnumerable<string> bccs,
            string body, string from = "", string subject = "");
    }
}
