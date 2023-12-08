using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA1WorkflowHistoryItems.CreateSMTA1WorkflowHistoryItem;

public interface ICreateSMTA1WorkflowHistoryItemMapper
{
    SMTA1WorkflowHistoryItem Map(CreateSMTA1WorkflowHistoryItemCommand command);
}

public class CreateSMTA1WorkflowHistoryItemMapper : ICreateSMTA1WorkflowHistoryItemMapper
{
    public SMTA1WorkflowHistoryItem Map(CreateSMTA1WorkflowHistoryItemCommand command)
    {
        // TODO: Implement mapper

        SMTA1WorkflowHistoryItem SMTA1WorkflowHistoryItem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return SMTA1WorkflowHistoryItem;
    }
}