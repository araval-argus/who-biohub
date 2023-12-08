using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine
{
    public interface IWorklistToBioHubEmailNotifier
    {
        Task<Errors?> NotifyUsers(WorklistToBioHubItem worklistToBioHubItem, CancellationToken cancellationToken);
    }
}