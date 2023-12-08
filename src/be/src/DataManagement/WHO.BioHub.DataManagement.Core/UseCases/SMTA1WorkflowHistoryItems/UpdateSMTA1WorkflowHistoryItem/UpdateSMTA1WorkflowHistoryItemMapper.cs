using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.UpdateSMTA1WorkflowHistoryItem;

public interface IUpdateSMTA1WorkflowHistoryItemMapper
{
    SMTA1WorkflowHistoryItem Map(SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem, UpdateSMTA1WorkflowHistoryItemCommand command);
}

public class UpdateSMTA1WorkflowHistoryItemMapper : IUpdateSMTA1WorkflowHistoryItemMapper
{
    public SMTA1WorkflowHistoryItem Map(SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem, UpdateSMTA1WorkflowHistoryItemCommand command)
    {
        // TODO: Implement mapper

        SMTA1WorkflowHistoryItem.Id = command.Id;
        SMTA1WorkflowHistoryItem.CreationDate = DateTime.UtcNow;

        // ...

        SMTA1WorkflowHistoryItem.DeletedOn = null;

        return SMTA1WorkflowHistoryItem;
    }
}