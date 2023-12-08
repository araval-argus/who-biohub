using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;

public record struct ListIsolationTechniqueTypesQueryResponse(IEnumerable<IsolationTechniqueTypeDto> IsolationTechniqueTypes) { }