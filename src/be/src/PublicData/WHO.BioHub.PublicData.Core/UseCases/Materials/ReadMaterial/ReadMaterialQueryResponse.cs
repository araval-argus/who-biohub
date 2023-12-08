using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Materials.ReadMaterial;

public record struct ReadMaterialQueryResponse(MaterialPublicViewModel Material) { }