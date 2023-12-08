using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.CreateDocument;

public interface ICreateDocumentMapper
{
    Document Map(CreateDocumentCommand command);
}

public class CreateDocumentMapper : ICreateDocumentMapper
{
    public Document Map(CreateDocumentCommand command)
    {
        // TODO: Implement mapper

        Document document = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return document;
    }
}