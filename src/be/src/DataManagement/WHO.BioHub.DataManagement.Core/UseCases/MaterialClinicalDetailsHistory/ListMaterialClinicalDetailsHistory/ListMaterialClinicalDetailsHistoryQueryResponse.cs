using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialClinicalDetailsHistory.ListMaterialClinicalDetailsHistory;

public record struct ListMaterialClinicalDetailsHistoryQueryResponse(IEnumerable<MaterialClinicalDetailHistory> MaterialClinicalDetailsHistory) { }