using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHO.BioHub.StorageAccount
{
    public class StorageConfiguration
    {
        public string? DocumentTemplatesConnection { get; set; }
        public string? DocumentTemplatesContainerReference { get; set; }
        public string? DocumentsConnection { get; set; }
        public string? DocumentsContainerReference { get; set; }
        public string? PublicDocumentsConnectionBaseUrl { get; set; }
        public string? PublicDocumentsAccountReference { get; set; }
        public string? PublicDocumentsContainerReference { get; set; }
        public string? PublicDocumentsConnectionKey { get; set; }
        public int PublicDocumentsSasExpirationMinutes { get; set; }

    }
}
