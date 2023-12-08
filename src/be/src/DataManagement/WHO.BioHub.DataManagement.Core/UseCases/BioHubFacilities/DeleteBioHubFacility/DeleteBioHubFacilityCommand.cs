namespace WHO.BioHub.DataManagement.Core.UseCases.BioHubFacilities.DeleteBioHubFacility;

public record struct DeleteBioHubFacilityCommand(Guid Id, Guid? OperationById) { }