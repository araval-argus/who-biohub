using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;

public record struct CreateWorklistToBioHubItemCommand(
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    Guid? UserId,
    bool? IsPast,
    DateTime? AssignedOperationDate);