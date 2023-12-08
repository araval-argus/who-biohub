using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.IsolationTechniqueTypes.ListIsolationTechniqueTypes;

public record struct ListIsolationTechniqueTypesQueryResponse(IEnumerable<IsolationTechniqueType> IsolationTechniqueTypes) { }