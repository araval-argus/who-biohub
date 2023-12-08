using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistFromBioHubItems.UpdateWorklistFromBioHubItemQEShipmentDocuments;

public interface IUpdateWorklistFromBioHubItemQEShipmentDocumentsMapper
{
    WorklistFromBioHubItem Map(WorklistFromBioHubItem worklistfrombiohubitem, UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand command);
}

public class UpdateWorklistFromBioHubItemQEShipmentDocumentsMapper : IUpdateWorklistFromBioHubItemQEShipmentDocumentsMapper
{
    public WorklistFromBioHubItem Map(
        WorklistFromBioHubItem worklistfrombiohubitem,
        UpdateWorklistFromBioHubItemQEShipmentDocumentsCommand command
        )
    {       


        return worklistfrombiohubitem;
    }
}