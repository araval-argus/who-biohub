using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UpdateDocumentTemplate;

public interface IUpdateDocumentTemplateMapper
{
    DocumentTemplate Map(DocumentTemplate documenttemplate, UpdateDocumentTemplateCommand command);
}

public class UpdateDocumentTemplateMapper : IUpdateDocumentTemplateMapper
{
    public DocumentTemplate Map(DocumentTemplate documenttemplate, UpdateDocumentTemplateCommand command)
    {
        // TODO: Implement mapper

        documenttemplate.Id = command.Id;
        documenttemplate.CreationDate = DateTime.UtcNow;

        // ...

        documenttemplate.DeletedOn = null;

        return documenttemplate;
    }
}