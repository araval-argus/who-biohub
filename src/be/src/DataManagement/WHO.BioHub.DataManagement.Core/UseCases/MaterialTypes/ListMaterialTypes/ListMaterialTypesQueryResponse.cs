using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.ListMaterialTypes;

public record struct ListMaterialTypesQueryResponse(IEnumerable<MaterialTypeDto> MaterialTypes) { }