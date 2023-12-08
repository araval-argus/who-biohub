using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.CreateSMTA1WorkflowItem;

public interface ICreateSMTA1WorkflowItemMapper
{
    SMTA1WorkflowItem Map(CreateSMTA1WorkflowItemCommand command);
}

public class CreateSMTA1WorkflowItemMapper : ICreateSMTA1WorkflowItemMapper
{
    public SMTA1WorkflowItem Map(CreateSMTA1WorkflowItemCommand command)
    {       

        SMTA1WorkflowItem SMTA1WorkflowItem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            OperationDate = command.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow,
            LaboratoryId = command.LaboratoryId,
            LastOperationUserId = command.UserId,
            LastSubmissionApproved = true,
            WorkflowItemTitle = SMTA1WorkflowStatus.RequestInitiation.WorklistItemApprovedInfo(),
            PreviousStatus = SMTA1WorkflowStatus.RequestInitiation,
            DeletedOn = null,
            ReferenceId = Guid.NewGuid(),
            IsPast = command.IsPast,
        };

        return SMTA1WorkflowItem;
    }
}