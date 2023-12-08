using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetails.ListMaterialClinicalDetails;

public record struct ListMaterialClinicalDetailsQueryResponse(IEnumerable<MaterialClinicalDetail> MaterialClinicalDetails) { }