using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.CreateFolder;

public interface ICreateFolderMapper
{
    DocumentTemplate Map(CreateFolderCommand command);
}

public class CreateFolderMapper : ICreateFolderMapper
{
    public DocumentTemplate Map(CreateFolderCommand command)
    {     

        DocumentTemplate documenttemplate = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            UploadTime = DateTime.UtcNow,
            Type = DocumentTemplateType.Folder,
            Name = command.Name,
            ParentId = command.ParentId,
            DeletedOn = null,
        };

        return documenttemplate;
    }
}