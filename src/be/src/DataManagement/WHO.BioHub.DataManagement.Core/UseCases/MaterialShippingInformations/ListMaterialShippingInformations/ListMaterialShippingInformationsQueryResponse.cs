using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.ListMaterialShippingInformations;

public record struct ListMaterialShippingInformationsQueryResponse(IEnumerable<MaterialShippingInformation> MaterialShippingInformations) { }