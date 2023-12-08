using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;

public interface IWorklistFromBioHubItemReadRepository
{
    Task<WorklistFromBioHubItem> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> List(CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadByIdAndStatus(Guid id, WorklistFromBioHubStatus status, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken);
    Task<int?> GetTotalRequestsOfTheYear(CancellationToken cancellationToken);
    Task<IEnumerable<Annex2OfSMTA2Condition>> GetAnnex2OfSMTA2ConditionList(CancellationToken cancellationToken);
    Task<IEnumerable<BiosafetyChecklistOfSMTA2>> GetBiosafetyChecklistOfSMTA2List(CancellationToken cancellationToken);
    Task<WorkflowEmailInfoDto> ReadInfoForEmail(Guid id, WorklistFromBioHubStatus toStatus, WorklistFromBioHubStatus fromStatus, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadWithHistory(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Annex2OfSMTA2Condition>> GetAllAnnex2OfSMTA2ConditionList(CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadAnnex2OfSMTA2Info(Guid id, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadAnnex2OfSMTA2InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadAnnex2OfSMTA2InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadBookingFormOfSMTA2Info(Guid id, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadBookingFormOfSMTA2InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadBookingFormOfSMTA2InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadBiosafetyChecklistOfSMTA2Info(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BiosafetyChecklistOfSMTA2>> GetAllBiosafetyChecklistOfSMTA2List(CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadBiosafetyChecklistOfSMTA2InfoForBioHubFacility(Guid id, Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<WorklistFromBioHubItem> ReadBiosafetyChecklistOfSMTA2InfoForLaboratory(Guid id, Guid laboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> ReadForEformList(CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> ReadForEformListForBioHubFacility(Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> ReadForEformListForLaboratory(Guid laboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> ListByIds(List<Guid> ids, CancellationToken cancellationToken);
}
