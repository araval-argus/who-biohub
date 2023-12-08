using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine
{
    public interface ISMTA2WorkflowEmailNotifier
    {
        Task<Errors?> NotifyUsers(SMTA2WorkflowItem SMTA2WorkflowItem, CancellationToken cancellationToken);
    }
}