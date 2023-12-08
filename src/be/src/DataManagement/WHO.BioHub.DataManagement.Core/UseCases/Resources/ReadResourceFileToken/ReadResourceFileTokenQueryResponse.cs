using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.ReadResourceFileToken;

public record struct ReadResourceFileTokenQueryResponse(string FileToken, string FileCompleteName) { }