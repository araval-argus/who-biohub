using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.CultivabilityTypes.ListCultivabilityTypes;

public record struct ListCultivabilityTypesQueryResponse(IEnumerable<CultivabilityTypeDto> CultivabilityTypes) { }