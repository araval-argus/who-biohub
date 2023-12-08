using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateResource;

public record struct CreateResourceCommand (
    Guid? Id,
    Guid? ParentId,
    Guid UploadedById,
    ResourceFileType? FileType,  
    string FileCompleteName
   );