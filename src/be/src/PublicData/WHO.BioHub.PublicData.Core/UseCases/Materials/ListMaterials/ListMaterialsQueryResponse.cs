using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ListMaterials;

public record struct ListMaterialsQueryResponse(IEnumerable<MaterialPublicViewModel> Materials) { }