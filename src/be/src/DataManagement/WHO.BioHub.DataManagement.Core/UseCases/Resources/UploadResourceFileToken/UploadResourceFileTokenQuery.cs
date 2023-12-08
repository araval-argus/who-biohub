using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.UploadResourceFileToken;

public record struct UploadResourceFileTokenQuery(string FileCompleteName, ResourceFileType? FileType) { }