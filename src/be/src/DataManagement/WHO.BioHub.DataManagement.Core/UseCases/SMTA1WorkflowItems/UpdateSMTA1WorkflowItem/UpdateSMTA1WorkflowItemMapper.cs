using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowItems.UpdateSMTA1WorkflowItem;

public interface IUpdateSMTA1WorkflowItemMapper
{
    SMTA1WorkflowItem Map(SMTA1WorkflowItem SMTA1WorkflowItem, UpdateSMTA1WorkflowItemCommand command);
}

public class UpdateSMTA1WorkflowItemMapper : IUpdateSMTA1WorkflowItemMapper
{
    public SMTA1WorkflowItem Map(
        SMTA1WorkflowItem SMTA1WorkflowItem,
        UpdateSMTA1WorkflowItemCommand command
        )
    {
        SMTA1WorkflowItem.LastOperationUserId = command.IsSaveDraft != true ? command.UserId : SMTA1WorkflowItem.LastOperationUserId;
        SMTA1WorkflowItem.OperationDate = command.IsSaveDraft != true ? (SMTA1WorkflowItem.IsPast == true ? command.AssignedOperationDate : DateTime.UtcNow) : SMTA1WorkflowItem.OperationDate;
        SMTA1WorkflowItem.LastSubmissionApproved = command.IsSaveDraft != true ? command.LastSubmissionApproved : SMTA1WorkflowItem.LastSubmissionApproved;
        SMTA1WorkflowItem.PreviousStatus = command.IsSaveDraft != true ? SMTA1WorkflowItem.Status : SMTA1WorkflowItem.PreviousStatus;
        SMTA1WorkflowItem.WorkflowItemTitle = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? SMTA1WorkflowItem.Status.WorklistItemApprovedInfo() : SMTA1WorkflowItem.Status.WorklistItemRejectedInfo()) : SMTA1WorkflowItem.WorkflowItemTitle;
        SMTA1WorkflowItem.Comment = command.IsSaveDraft != true ? (command.LastSubmissionApproved == true ? string.Empty : command.Comment) : SMTA1WorkflowItem.Comment;
        SMTA1WorkflowItem.ReferenceId = Guid.NewGuid();



        return SMTA1WorkflowItem;
    }
}