using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ReadBSLLevel;

public record struct ReadBSLLevelQueryResponse(BSLLevelDto BSLLevel) { }