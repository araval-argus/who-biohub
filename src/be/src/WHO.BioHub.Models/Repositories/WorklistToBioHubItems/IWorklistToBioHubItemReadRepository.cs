using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.WorklistToBioHubItems;

public interface IWorklistToBioHubItemReadRepository
{
    Task<WorklistToBioHubItem> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> List(CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadByIdAndStatus(Guid id, WorklistToBioHubStatus status, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken);
    Task<int?> GetTotalRequestsOfTheYear(CancellationToken cancellationToken);
    Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, WorklistToBioHubStatus toStatus, WorklistToBioHubStatus fromStatus, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadWithHistory(Guid id, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadAnnex2OfSMTA1Info(Guid id, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadBookingFormOfSMTA1Info(Guid id, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadBookingFormOfSMTA1InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadBookingFormOfSMTA1InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadAnnex2OfSMTA1InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<WorklistToBioHubItem> ReadAnnex2OfSMTA1InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> ReadForEformList(CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> ReadForEformListForBioHubFacility(Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> ReadForEformListForLaboratory(Guid laboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> ListByIds(List<Guid> ids, CancellationToken cancellationToken);
}
