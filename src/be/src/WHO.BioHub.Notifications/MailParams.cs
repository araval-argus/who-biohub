using System.Text;

namespace WHO.BioHub.Notifications
{
    public class MailParams
    {
        public string Subject { get; set; }
        public Encoding SubjectEncoding { get; set; }
        public string From { get; set; }
        public string[] To { get; set; }
        public string[] Cc { get; set; }
        public string[] Bcc { get; set; }
        public string Body { get; set; }
        public Encoding BodyEncoding { get; set; }
        public bool? IsBodyHtml { get; set; }

        // TODO: further evolution
        //public string Template { get; set; }
        //public Dictionary<string, string> TemplateParams { get; set; }
    }
}
