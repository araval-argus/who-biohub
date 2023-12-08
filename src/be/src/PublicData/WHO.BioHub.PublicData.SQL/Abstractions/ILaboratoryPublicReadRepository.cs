using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface ILaboratoryPublicReadRepository
{
    Task<Laboratory> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Laboratory>> List(CancellationToken cancellationToken);
    Task<IEnumerable<Laboratory>> ListMap(CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> GetWorklistToBioHubItemsForMap(Guid laboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> GetWorklistFromBioHubItemsForMap(Guid laboratoryId, CancellationToken cancellationToken);
}
