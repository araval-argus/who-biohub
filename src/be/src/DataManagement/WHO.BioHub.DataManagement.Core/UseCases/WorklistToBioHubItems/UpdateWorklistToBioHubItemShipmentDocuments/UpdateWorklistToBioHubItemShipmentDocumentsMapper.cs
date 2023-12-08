using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.UpdateWorklistToBioHubItemShipmentDocuments;

public interface IUpdateWorklistToBioHubItemShipmentDocumentsMapper
{
    WorklistToBioHubItem Map(WorklistToBioHubItem worklisttobiohubitem, UpdateWorklistToBioHubItemShipmentDocumentsCommand command);
}

public class UpdateWorklistToBioHubItemShipmentDocumentsMapper : IUpdateWorklistToBioHubItemShipmentDocumentsMapper
{
    public WorklistToBioHubItem Map(
        WorklistToBioHubItem worklisttobiohubitem,
        UpdateWorklistToBioHubItemShipmentDocumentsCommand command
        )
    {       

        return worklisttobiohubitem;
    }
}