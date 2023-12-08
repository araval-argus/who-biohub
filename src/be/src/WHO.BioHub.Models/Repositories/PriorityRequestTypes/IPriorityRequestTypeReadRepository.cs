using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.PriorityRequestTypes;

public interface IPriorityRequestTypeReadRepository
{
    Task<PriorityRequestType> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<PriorityRequestType>> List(CancellationToken cancellationToken);
}
