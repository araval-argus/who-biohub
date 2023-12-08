using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Resources;

public interface IResourceWriteRepository
{
    Task<Either<Resource, Errors>> Create(Resource resource, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<Errors?> DeleteRange(IEnumerable<Guid> ids, CancellationToken cancellationToken);
    Task<List<Guid>> GetIdsForDelete(IEnumerable<Guid> parentIds, CancellationToken cancellationToken);
    Task<Resource> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(Resource resource, CancellationToken cancellationToken);
}
