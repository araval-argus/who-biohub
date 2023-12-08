using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.ListMaterialProducts;

public record struct ListMaterialProductsQueryResponse(IEnumerable<MaterialProductDto> MaterialProducts) { }