using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.CreateWorklistToBioHubItem;

public interface ICreateWorklistToBioHubItemMapper
{
    WorklistToBioHubItem Map(CreateWorklistToBioHubItemCommand command);
}

public class CreateWorklistToBioHubItemMapper : ICreateWorklistToBioHubItemMapper
{
    public WorklistToBioHubItem Map(CreateWorklistToBioHubItemCommand command)
    {

        WorklistToBioHubItem worklisttobiohubitem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            OperationDate = command.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow,
            RequestInitiationFromLaboratoryId = command.LaboratoryId,
            LastOperationUserId = command.UserId,
            LastSubmissionApproved = true,
            WorklistItemTitle = WorklistToBioHubStatus.RequestInitiation.WorklistItemApprovedInfo(),
            PreviousStatus = WorklistToBioHubStatus.RequestInitiation,
            DeletedOn = null,
            ReferenceId = Guid.NewGuid(),
            IsPast = command.IsPast,
           
        };

        return worklisttobiohubitem;
    }
}