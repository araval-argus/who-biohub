using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemBHFShipmentDocuments;

public interface IUpdateWorklistFromBioHubItemBHFShipmentDocumentsMapper
{
    WorklistFromBioHubItem Map(WorklistFromBioHubItem worklistfrombiohubitem, UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand command);
}

public class UpdateWorklistFromBioHubItemBHFShipmentDocumentsMapper : IUpdateWorklistFromBioHubItemBHFShipmentDocumentsMapper
{
    public WorklistFromBioHubItem Map(
        WorklistFromBioHubItem worklistfrombiohubitem,
        UpdateWorklistFromBioHubItemBHFShipmentDocumentsCommand command
        )
    {



        return worklistfrombiohubitem;
    }
}