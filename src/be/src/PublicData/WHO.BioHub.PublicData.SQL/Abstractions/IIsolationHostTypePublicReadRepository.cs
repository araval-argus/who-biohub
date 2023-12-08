using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IIsolationHostTypePublicReadRepository
{
    Task<IsolationHostType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<IsolationHostType>> List(CancellationToken cancellationToken);
}
