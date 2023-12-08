using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowHistoryItems.CreateSMTA2WorkflowHistoryItem;

public interface ICreateSMTA2WorkflowHistoryItemMapper
{
    SMTA2WorkflowHistoryItem Map(CreateSMTA2WorkflowHistoryItemCommand command);
}

public class CreateSMTA2WorkflowHistoryItemMapper : ICreateSMTA2WorkflowHistoryItemMapper
{
    public SMTA2WorkflowHistoryItem Map(CreateSMTA2WorkflowHistoryItemCommand command)
    {
        // TODO: Implement mapper

        SMTA2WorkflowHistoryItem smta2workflowhistoryitem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return smta2workflowhistoryitem;
    }
}