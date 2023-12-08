using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.ListBSLLevels;

public record struct ListBSLLevelsQueryResponse(IEnumerable<BSLLevelDto> BSLLevels) { }