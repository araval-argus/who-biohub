using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ListMaterials;

public record struct ListMaterialsQueryResponse(IEnumerable<MaterialViewModel> Materials) { }