using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.BioHubFacilities;

public interface IBioHubFacilityReadRepository
{
    Task<BioHubFacility> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BioHubFacility>> List(CancellationToken cancellationToken);
    Task<IEnumerable<BioHubFacility>> ListForLaboratoryUser(CancellationToken cancellationToken);
    Task<IEnumerable<BioHubFacility>> ListMapForLaboratoryUser(CancellationToken cancellationToken);
    Task<IEnumerable<BioHubFacility>> ListMap(CancellationToken cancellationToken);
    Task<BioHubFacilityHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> GetWorklistToBioHubItemsForMap(Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> GetWorklistFromBioHubItemsForMap(Guid bioHubFacilityId, CancellationToken cancellationToken);
}
