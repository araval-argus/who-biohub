using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IBioHubFacilityPublicReadRepository
{
    Task<BioHubFacility> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<BioHubFacility>> List(CancellationToken cancellationToken);
    Task<IEnumerable<BioHubFacility>> ListMap(CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> GetWorklistToBioHubItemsForMap(Guid bioHubFacilityId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> GetWorklistFromBioHubItemsForMap(Guid bioHubFacilityId, CancellationToken cancellationToken);
}
