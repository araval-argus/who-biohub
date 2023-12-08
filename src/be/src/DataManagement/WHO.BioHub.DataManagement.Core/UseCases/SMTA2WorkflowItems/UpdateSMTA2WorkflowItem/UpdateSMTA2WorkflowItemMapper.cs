using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.UpdateSMTA2WorkflowItem;

public interface IUpdateSMTA2WorkflowItemMapper
{
    SMTA2WorkflowItem Map(SMTA2WorkflowItem SMTA2WorkflowItem, UpdateSMTA2WorkflowItemCommand command);
}

public class UpdateSMTA2WorkflowItemMapper : IUpdateSMTA2WorkflowItemMapper
{
    public SMTA2WorkflowItem Map(
        SMTA2WorkflowItem SMTA2WorkflowItem,
        UpdateSMTA2WorkflowItemCommand command
        )
    {
        SMTA2WorkflowItem.LastOperationUserId = command.IsSaveDraft != true ? command.UserId : SMTA2WorkflowItem.LastOperationUserId;
        SMTA2WorkflowItem.OperationDate = command.IsSaveDraft != true ? (SMTA2WorkflowItem.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow) : SMTA2WorkflowItem.OperationDate;
        SMTA2WorkflowItem.LastSubmissionApproved = command.IsSaveDraft != true ? command.LastSubmissionApproved : SMTA2WorkflowItem.LastSubmissionApproved;
        SMTA2WorkflowItem.PreviousStatus = command.IsSaveDraft != true ? SMTA2WorkflowItem.Status : SMTA2WorkflowItem.PreviousStatus;
        SMTA2WorkflowItem.WorkflowItemTitle = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? SMTA2WorkflowItem.Status.WorklistItemApprovedInfo() : SMTA2WorkflowItem.Status.WorklistItemRejectedInfo()) : SMTA2WorkflowItem.WorkflowItemTitle;
        SMTA2WorkflowItem.Comment = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? string.Empty : command.Comment) : SMTA2WorkflowItem.Comment;
        SMTA2WorkflowItem.ReferenceId = Guid.NewGuid();



        return SMTA2WorkflowItem;
    }
}