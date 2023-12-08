using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Laboratories;

public interface ILaboratoryReadRepository
{
    Task<Laboratory> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Laboratory>> List(CancellationToken cancellationToken);
    Task<IEnumerable<Laboratory>> ListForLaboratoryUser(Guid? laboratoryId, CancellationToken cancellationToken);
    Task<Laboratory> ReadForLaboratoryUser(Guid id, Guid UserLaboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<Laboratory>> ListMapForLaboratoryUser(Guid? laboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<Laboratory>> ListMap(CancellationToken cancellationToken);
    Task<LaboratoryHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistToBioHubItem>> GetWorklistToBioHubItemsForMap(Guid laboratoryId, CancellationToken cancellationToken);
    Task<IEnumerable<WorklistFromBioHubItem>> GetWorklistFromBioHubItemsForMap(Guid laboratoryId, CancellationToken cancellationToken);
}
