using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.UploadResourceFileToken;

public record struct UploadResourceFileTokenQueryResponse(Guid Id, string FileToken, string FileCompleteName) { }