using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.IsolationHostTypes;

public interface IIsolationHostTypeReadRepository
{
    Task<IsolationHostType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<IsolationHostType>> List(CancellationToken cancellationToken);
}
