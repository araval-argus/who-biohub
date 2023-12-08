using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.UpdateDocument;

public interface IUpdateDocumentMapper
{
    Document Map(Document document, UpdateDocumentCommand command);
}

public class UpdateDocumentMapper : IUpdateDocumentMapper
{
    public Document Map(Document document, UpdateDocumentCommand command)
    {
        // TODO: Implement mapper

        document.Id = command.Id;
        document.CreationDate = DateTime.UtcNow;

        // ...

        document.DeletedOn = null;

        return document;
    }
}