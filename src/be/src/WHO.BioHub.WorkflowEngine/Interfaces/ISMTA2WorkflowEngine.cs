using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public interface ISMTA2WorkflowEngine
    {
        Task<SMTA2WorkflowItem> MoveToNextStatusUponApproveOrSaveDraft(SMTA2WorkflowItem SMTA2WorkflowItem, MoveToNextStatusSMTA2WorkflowEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
        Task<SMTA2WorkflowItem> MoveToNextStatusUponReject(SMTA2WorkflowItem SMTA2WorkflowItem, MoveToNextStatusSMTA2WorkflowEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    }
}