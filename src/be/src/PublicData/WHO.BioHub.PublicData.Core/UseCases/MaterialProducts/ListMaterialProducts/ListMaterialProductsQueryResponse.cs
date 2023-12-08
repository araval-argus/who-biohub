using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.MaterialProducts.ListMaterialProducts;

public record struct ListMaterialProductsQueryResponse(IEnumerable<MaterialProductPublicDto> MaterialProducts) { }