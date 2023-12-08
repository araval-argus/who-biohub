namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratoryFromUserRequest;

public record struct CreateLaboratoryFromUserRequestCommand(
    string InstituteName,
    Guid? CountryId,
    Guid? OperationById
);
