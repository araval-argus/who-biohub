using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IPriorityRequestTypePublicReadRepository
{
    Task<PriorityRequestType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<PriorityRequestType>> List(CancellationToken cancellationToken);
}
