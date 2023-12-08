using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.UpdateWorklistFromBioHubHistoryItem;

public interface IUpdateWorklistFromBioHubHistoryItemMapper
{
    WorklistFromBioHubHistoryItem Map(WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem, UpdateWorklistFromBioHubHistoryItemCommand command);
}

public class UpdateWorklistFromBioHubHistoryItemMapper : IUpdateWorklistFromBioHubHistoryItemMapper
{
    public WorklistFromBioHubHistoryItem Map(WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem, UpdateWorklistFromBioHubHistoryItemCommand command)
    {
        // TODO: Implement mapper

        worklistfrombiohubhistoryitem.Id = command.Id;
        worklistfrombiohubhistoryitem.CreationDate = DateTime.UtcNow;

        // ...

        worklistfrombiohubhistoryitem.DeletedOn = null;

        return worklistfrombiohubhistoryitem;
    }
}