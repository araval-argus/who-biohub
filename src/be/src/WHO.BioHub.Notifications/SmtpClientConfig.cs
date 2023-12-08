namespace WHO.BioHub.Notifications
{
    public class SmtpClientConfig
    {
        public string Smtp { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Port { get; set; }

        public bool EnableSsl { get; set; }
    }
}
