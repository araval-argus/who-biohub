using System.Text;

namespace WHO.BioHub.Notifications
{
    public enum EmailProviderEnum
    {
        FAKE = 0,
        GOOGLE = 1,
        SEND_GRID = 2
    }

    public class MailServiceConfig
    {
        public string DefaultFrom { get; set; }
        public string DefaultSubject { get; set; }
        public Encoding DefaultBodyEncoding => Encoding.UTF8;
        public Encoding DefaultSubjectEncoding => Encoding.UTF8;
        public bool DefaultIsBodyHtml => true;
        public string DefaultProvider { get; set; }
        public EmailProviderEnum EmailProviderEnum { get; set; }
    }
}