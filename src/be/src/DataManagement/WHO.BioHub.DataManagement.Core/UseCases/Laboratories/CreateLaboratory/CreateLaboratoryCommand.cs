namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratory;

public record struct CreateLaboratoryCommand(
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
     Guid? OperationById
);
