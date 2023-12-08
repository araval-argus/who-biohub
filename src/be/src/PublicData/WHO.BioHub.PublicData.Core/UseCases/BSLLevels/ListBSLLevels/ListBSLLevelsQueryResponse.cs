using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.BSLLevels.ListBSLLevels;

public record struct ListBSLLevelsQueryResponse(IEnumerable<BSLLevel> BSLLevels) { }