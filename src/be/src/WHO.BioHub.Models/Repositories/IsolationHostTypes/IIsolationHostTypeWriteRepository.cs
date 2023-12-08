using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.IsolationHostTypes;

public interface IIsolationHostTypeWriteRepository
{
    Task<Either<IsolationHostType, Errors>> Create(IsolationHostType isolationhosttype, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<IsolationHostType> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(IsolationHostType isolationhosttype, CancellationToken cancellationToken);
}
