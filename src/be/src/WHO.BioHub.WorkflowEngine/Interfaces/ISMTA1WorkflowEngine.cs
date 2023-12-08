using Microsoft.EntityFrameworkCore.Storage;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;
using WHO.BioHub.WorkflowEngine.Commands;

namespace WHO.BioHub.WorkflowEngine
{
    public interface ISMTA1WorkflowEngine
    {
        Task<SMTA1WorkflowItem> MoveToNextStatusUponApproveOrSaveDraft(SMTA1WorkflowItem SMTA1WorkflowItem, MoveToNextStatusSMTA1WorkflowEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
        Task<SMTA1WorkflowItem> MoveToNextStatusUponReject(SMTA1WorkflowItem SMTA1WorkflowItem, MoveToNextStatusSMTA1WorkflowEngineCommand command, CancellationToken cancellationToken, IDbContextTransaction? transaction = null);
    }
}