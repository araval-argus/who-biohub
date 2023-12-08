namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.UpdateCourier;

public record struct UpdateCourierCommand(Guid Id,
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
    Guid? OperationById);