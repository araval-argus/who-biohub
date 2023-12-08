using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.WorklistFromBioHubEmails;

public interface IWorklistFromBioHubEmailReadRepository
{
    Task<WorklistFromBioHubEmail> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubEmail>> List(CancellationToken cancellationToken);
    Task<WorklistFromBioHubEmail> ReadByStatusRoleApproved(WorklistFromBioHubStatus fromStatus, WorklistFromBioHubStatus toStatus, bool approvedSubmission, Guid roleId, CancellationToken cancellationToken, bool isCourier = false, bool IsNumberOfVialsWarning = false);
}
