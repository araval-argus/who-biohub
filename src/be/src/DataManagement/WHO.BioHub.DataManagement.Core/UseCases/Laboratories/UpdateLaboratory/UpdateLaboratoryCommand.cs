namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.UpdateLaboratory;

public record struct UpdateLaboratoryCommand(
    Guid Id,
    string Name,
    string Description,
    string Abbreviation,
    string Address,
    double? Latitude,
    double? Longitude,
    bool IsActive,
    Guid? BSLLevelId,
    Guid? CountryId,
    bool IsPublicFacing, 
    Guid? OperationById);