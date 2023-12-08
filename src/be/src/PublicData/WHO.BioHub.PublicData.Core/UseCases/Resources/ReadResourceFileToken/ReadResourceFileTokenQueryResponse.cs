using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.Resources.ReadResourceFileToken;

public record struct ReadResourceFileTokenQueryResponse(string FileToken, string FileCompleteName, string Extension) { }