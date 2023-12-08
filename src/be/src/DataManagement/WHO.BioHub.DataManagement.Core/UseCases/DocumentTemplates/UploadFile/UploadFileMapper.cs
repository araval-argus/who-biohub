using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UploadFile;

public interface IUploadFileMapper
{
    DocumentTemplate Map(UploadFileCommand command);
}

public class UploadFileMapper : IUploadFileMapper
{
    public DocumentTemplate Map(UploadFileCommand command)
    {        

        DocumentTemplate documenttemplate = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            UploadTime = DateTime.UtcNow,
            Type = DocumentTemplateType.File,
            FileType = command.DocumentTemplateFileType,
            Name = Path.GetFileNameWithoutExtension(command.File.FileName),
            Extension = (Path.GetExtension(command.File.FileName).Replace(".", "")),
            UploadedById = command.UploadedById,
            ParentId = command.ParentId,
            DeletedOn = null,
        };

        return documenttemplate;
    }
}