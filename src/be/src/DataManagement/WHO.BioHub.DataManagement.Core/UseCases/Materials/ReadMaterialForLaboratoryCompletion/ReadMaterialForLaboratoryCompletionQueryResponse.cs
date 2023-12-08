using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForLaboratoryCompletion;

public record struct ReadMaterialForLaboratoryCompletionQueryResponse(MaterialForLaboratoryCompletionViewModel Material) { }