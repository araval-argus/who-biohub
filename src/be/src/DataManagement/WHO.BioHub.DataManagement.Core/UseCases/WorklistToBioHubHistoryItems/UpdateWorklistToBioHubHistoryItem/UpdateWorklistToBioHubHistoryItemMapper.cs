using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubHistoryItems.UpdateWorklistToBioHubHistoryItem;

public interface IUpdateWorklistToBioHubHistoryItemMapper
{
    WorklistToBioHubHistoryItem Map(WorklistToBioHubHistoryItem worklisttobiohubhistoryitem, UpdateWorklistToBioHubHistoryItemCommand command);
}

public class UpdateWorklistToBioHubHistoryItemMapper : IUpdateWorklistToBioHubHistoryItemMapper
{
    public WorklistToBioHubHistoryItem Map(WorklistToBioHubHistoryItem worklisttobiohubhistoryitem, UpdateWorklistToBioHubHistoryItemCommand command)
    {
        // TODO: Implement mapper

        worklisttobiohubhistoryitem.Id = command.Id;
        worklisttobiohubhistoryitem.CreationDate = DateTime.UtcNow;

        // ...

        worklisttobiohubhistoryitem.DeletedOn = null;

        return worklisttobiohubhistoryitem;
    }
}