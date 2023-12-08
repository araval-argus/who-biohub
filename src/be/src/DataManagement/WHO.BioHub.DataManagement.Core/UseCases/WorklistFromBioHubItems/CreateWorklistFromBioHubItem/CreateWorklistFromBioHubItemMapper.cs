using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.CreateWorklistFromBioHubItem;
public interface ICreateWorklistFromBioHubItemMapper
{
    WorklistFromBioHubItem Map(CreateWorklistFromBioHubItemCommand command);
}
public class CreateWorklistFromBioHubItemMapper : ICreateWorklistFromBioHubItemMapper
{
    public WorklistFromBioHubItem Map(CreateWorklistFromBioHubItemCommand command)
    {

        WorklistFromBioHubItem worklisttobiohubitem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            OperationDate = command.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow,
            RequestInitiationToLaboratoryId = command.LaboratoryId,
            RequestInitiationFromBioHubFacilityId = command.BioHubFacilityId,
            LastOperationUserId = command.UserId,
            LastSubmissionApproved = true,
            WorklistItemTitle = WorklistFromBioHubStatus.RequestInitiation.WorklistItemApprovedInfo(),
            PreviousStatus = WorklistFromBioHubStatus.RequestInitiation,
            DeletedOn = null,
            ReferenceId = Guid.NewGuid(),
            IsPast = command.IsPast,
        };

        return worklisttobiohubitem;
    }
}