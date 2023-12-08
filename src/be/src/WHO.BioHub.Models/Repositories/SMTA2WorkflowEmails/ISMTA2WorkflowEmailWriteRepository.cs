using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Models.Repositories.SMTA2WorkflowEmails
{
    public interface ISMTA2WorkflowEmailWriteRepository
    {
        Task<Either<SMTA2WorkflowEmail, Errors>> Create(SMTA2WorkflowEmail SMTA2WorkflowEmail, CancellationToken cancellationToken);
        Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
        Task<SMTA2WorkflowEmail> ReadForUpdate(Guid id, CancellationToken cancellationToken);
        Task<Errors?> Update(SMTA2WorkflowEmail SMTA2WorkflowEmail, CancellationToken cancellationToken);
    }
}