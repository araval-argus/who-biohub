using System.Linq;
using WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocuments;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Documents.ListSignedSMTADocument;

public interface IListSignedSMTADocumentsMapper
{
    IEnumerable<DocumentItemDto> Map(List<Document> entities);
}

public class ListSignedSMTADocumentsMapper : IListSignedSMTADocumentsMapper
{
    public IEnumerable<DocumentItemDto> Map(List<Document> entities)
    {

        List<DocumentItemDto> listItems = new List<DocumentItemDto>();

        foreach (Document item in entities)
        {
            DocumentItemDto documentItemDto = new DocumentItemDto();
            documentItemDto.Id = item.Id;
            documentItemDto.Name = item.Name;
            documentItemDto.Extension = item.Extension;
            documentItemDto.FileType = item.Type;
            documentItemDto.UploadTime = GetUploadTime(item);
            listItems.Add(documentItemDto);
        }

        return listItems;
    }

    private DateTime? GetUploadTime(Document item)
    {
        if (item.SMTA1WorkflowItemDocuments != null && item.SMTA1WorkflowItemDocuments.Any())
        {
            return item.SMTA1WorkflowItemDocuments.FirstOrDefault(x => x.DocumentId == item.Id)?.Document?.OperationDate;
        }
        else
        {
            return item.SMTA2WorkflowItemDocuments.FirstOrDefault(x => x.DocumentId == item.Id)?.Document?.OperationDate;
        }
    }
}