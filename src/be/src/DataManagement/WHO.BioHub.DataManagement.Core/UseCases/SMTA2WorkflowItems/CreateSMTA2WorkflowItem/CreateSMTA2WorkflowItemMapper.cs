using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.CreateSMTA2WorkflowItem;

public interface ICreateSMTA2WorkflowItemMapper
{
    SMTA2WorkflowItem Map(CreateSMTA2WorkflowItemCommand command);
}

public class CreateSMTA2WorkflowItemMapper : ICreateSMTA2WorkflowItemMapper
{
    public SMTA2WorkflowItem Map(CreateSMTA2WorkflowItemCommand command)
    {
       
        SMTA2WorkflowItem SMTA2WorkflowItem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            OperationDate = command.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow,
            LaboratoryId = command.LaboratoryId,
            LastOperationUserId = command.UserId,
            LastSubmissionApproved = true,
            WorkflowItemTitle = SMTA2WorkflowStatus.RequestInitiation.WorklistItemApprovedInfo(),
            PreviousStatus = SMTA2WorkflowStatus.RequestInitiation,
            DeletedOn = null,
            ReferenceId = Guid.NewGuid(),
            IsPast = command.IsPast,
        };

        return SMTA2WorkflowItem;
    }
}