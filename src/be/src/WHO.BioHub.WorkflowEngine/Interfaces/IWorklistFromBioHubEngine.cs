using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public interface IWorklistFromBioHubEngine
    {
        Task<WorklistFromBioHubItem> MoveToNextStatusUponApproveOrSaveDraft(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
        Task<WorklistFromBioHubItem> MoveToNextStatusUponReject(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
        Task UpdateBHFShipmentDocuments(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken);
        Task UpdateQEShipmentDocuments(WorklistFromBioHubItem worklistFromBioHubItem, MoveToNextStatusFromBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken);
    }
}