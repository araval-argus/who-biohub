using Microsoft.AspNetCore.Http;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.DocumentTemplates.UploadFile;

public record struct UploadFileCommand(
    Guid? ParentId,
    Guid UploadedById,
    DocumentFileType? DocumentTemplateFileType,
    IFormFile File
);
