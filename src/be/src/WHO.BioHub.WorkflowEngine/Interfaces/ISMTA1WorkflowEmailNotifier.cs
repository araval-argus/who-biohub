using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine
{
    public interface ISMTA1WorkflowEmailNotifier
    {
        Task<Errors?> NotifyUsers(SMTA1WorkflowItem SMTA1WorkflowItem, CancellationToken cancellationToken);
    }
}