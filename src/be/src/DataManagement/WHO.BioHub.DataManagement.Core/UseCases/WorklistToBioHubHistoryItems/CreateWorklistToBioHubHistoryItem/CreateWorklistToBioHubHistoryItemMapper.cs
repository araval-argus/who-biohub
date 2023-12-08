using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.CreateWorklistToBioHubHistoryItem;

public interface ICreateWorklistToBioHubHistoryItemMapper
{
    WorklistToBioHubHistoryItem Map(CreateWorklistToBioHubHistoryItemCommand command);
}

public class CreateWorklistToBioHubHistoryItemMapper : ICreateWorklistToBioHubHistoryItemMapper
{
    public WorklistToBioHubHistoryItem Map(CreateWorklistToBioHubHistoryItemCommand command)
    {
        // TODO: Implement mapper

        WorklistToBioHubHistoryItem worklisttobiohubhistoryitem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return worklisttobiohubhistoryitem;
    }
}