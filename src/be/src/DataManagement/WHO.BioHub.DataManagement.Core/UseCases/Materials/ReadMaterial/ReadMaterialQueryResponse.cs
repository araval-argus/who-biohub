using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Materials.ReadMaterial;

public record struct ReadMaterialQueryResponse(MaterialViewModel Material) { }