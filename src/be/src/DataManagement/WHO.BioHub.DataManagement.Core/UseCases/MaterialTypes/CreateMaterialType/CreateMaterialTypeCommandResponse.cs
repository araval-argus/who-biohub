using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialTypes.CreateMaterialType;

public record struct CreateMaterialTypeCommandResponse(MaterialType MaterialType) { }