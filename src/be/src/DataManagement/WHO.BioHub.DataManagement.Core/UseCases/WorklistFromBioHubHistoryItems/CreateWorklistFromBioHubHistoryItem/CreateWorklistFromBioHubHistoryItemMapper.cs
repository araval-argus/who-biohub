using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubHistoryItems.CreateWorklistFromBioHubHistoryItem;

public interface ICreateWorklistFromBioHubHistoryItemMapper
{
    WorklistFromBioHubHistoryItem Map(CreateWorklistFromBioHubHistoryItemCommand command);
}

public class CreateWorklistFromBioHubHistoryItemMapper : ICreateWorklistFromBioHubHistoryItemMapper
{
    public WorklistFromBioHubHistoryItem Map(CreateWorklistFromBioHubHistoryItemCommand command)
    {
        // TODO: Implement mapper

        WorklistFromBioHubHistoryItem worklistfrombiohubhistoryitem = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return worklistfrombiohubhistoryitem;
    }
}