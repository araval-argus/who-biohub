using WHO.BioHub.Models.Models;

namespace WHO.BioHub.PublicData.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;

public record struct ListCultivabilityTypesQueryResponse(IEnumerable<CultivabilityType> CultivabilityTypes) { }