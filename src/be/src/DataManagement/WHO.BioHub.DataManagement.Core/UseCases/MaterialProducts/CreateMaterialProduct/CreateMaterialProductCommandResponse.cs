using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialProducts.CreateMaterialProduct;

public record struct CreateMaterialProductCommandResponse(MaterialProduct MaterialProduct) { }