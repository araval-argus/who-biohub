using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public interface IWorklistToBioHubEngine
    {
        Task<WorklistToBioHubItem> MoveToNextStatusUponApproveOrSaveDraft(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
        Task<WorklistToBioHubItem> MoveToNextStatusUponReject(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
        Task UpdateShipmentDocuments(WorklistToBioHubItem worklistToBioHubItem, MoveToNextStatusToBioHubEngineCommand command, IDbContextTransaction? transaction, CancellationToken cancellationToken);
    }
}