namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.CreateCourier;

public record struct CreateCourierCommand(
    string Name,
    string Description,
    string WHOAccountNumber,
    string Email,
    string BusinessPhone,
    string Address,
    double? Latitude,
    double? Longitude,
    bool IsActive,
    Guid? CountryId,
    Guid? OperationById
);