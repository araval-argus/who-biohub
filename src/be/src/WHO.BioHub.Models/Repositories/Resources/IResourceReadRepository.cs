using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.Resources;

public interface IResourceReadRepository
{
    Task<Resource> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Resource>> List(CancellationToken cancellationToken);
    Task<IEnumerable<Resource>> List(Guid id, CancellationToken cancellationToken);
}
