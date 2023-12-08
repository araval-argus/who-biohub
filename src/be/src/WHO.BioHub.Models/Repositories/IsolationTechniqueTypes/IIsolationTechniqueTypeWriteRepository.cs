using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.IsolationTechniqueTypes;

public interface IIsolationTechniqueTypeWriteRepository
{
    Task<Either<IsolationTechniqueType, Errors>> Create(IsolationTechniqueType isolationtechniquetype, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<IsolationTechniqueType> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(IsolationTechniqueType isolationtechniquetype, CancellationToken cancellationToken);
}
