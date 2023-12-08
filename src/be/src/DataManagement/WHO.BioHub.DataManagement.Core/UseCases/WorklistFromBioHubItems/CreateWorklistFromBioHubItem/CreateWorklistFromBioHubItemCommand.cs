using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;

public record struct CreateWorklistFromBioHubItemCommand(
    Guid? LaboratoryId,
    Guid? BioHubFacilityId,
    Guid? UserId,
    bool? IsPast,
    DateTime? AssignedOperationDate);