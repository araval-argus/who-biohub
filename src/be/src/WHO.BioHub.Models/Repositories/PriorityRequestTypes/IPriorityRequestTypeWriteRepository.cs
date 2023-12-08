using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.PriorityRequestTypes;

public interface IPriorityRequestTypeWriteRepository
{
    Task<Either<PriorityRequestType, Errors>> Create(PriorityRequestType priorityrequesttype, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<PriorityRequestType> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(PriorityRequestType priorityrequesttype, CancellationToken cancellationToken);
}
