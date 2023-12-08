using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.WorkflowEngine
{
    public interface IWorklistFromBioHubEmailNotifier
    {
        Task<Errors?> NotifyUsers(WorklistFromBioHubItem worklistFromBioHubItem, CancellationToken cancellationToken);
    }
}