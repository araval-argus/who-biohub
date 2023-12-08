using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterialForBioHubFacilityCompletion;

public record struct ReadMaterialForBioHubFacilityCompletionQueryResponse(MaterialViewModel Material) { }