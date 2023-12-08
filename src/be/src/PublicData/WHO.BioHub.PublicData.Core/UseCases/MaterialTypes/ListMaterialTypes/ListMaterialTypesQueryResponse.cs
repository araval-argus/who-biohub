using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialTypes.ListMaterialTypes;

public record struct ListMaterialTypesQueryResponse(IEnumerable<MaterialTypePublicDto> MaterialTypes) { }