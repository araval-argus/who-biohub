using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.UpdateSMTA2WorkflowHistoryItem;

public interface IUpdateSMTA2WorkflowHistoryItemMapper
{
    SMTA2WorkflowHistoryItem Map(SMTA2WorkflowHistoryItem smta2workflowhistoryitem, UpdateSMTA2WorkflowHistoryItemCommand command);
}

public class UpdateSMTA2WorkflowHistoryItemMapper : IUpdateSMTA2WorkflowHistoryItemMapper
{
    public SMTA2WorkflowHistoryItem Map(SMTA2WorkflowHistoryItem smta2workflowhistoryitem, UpdateSMTA2WorkflowHistoryItemCommand command)
    {
        // TODO: Implement mapper

        smta2workflowhistoryitem.Id = command.Id;
        smta2workflowhistoryitem.CreationDate = DateTime.UtcNow;

        // ...

        smta2workflowhistoryitem.DeletedOn = null;

        return smta2workflowhistoryitem;
    }
}