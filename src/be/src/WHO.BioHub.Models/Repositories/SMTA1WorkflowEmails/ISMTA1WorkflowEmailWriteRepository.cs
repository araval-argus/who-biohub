using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.Models.Repositories.SMTA1WorkflowEmails
{
    public interface ISMTA1WorkflowEmailWriteRepository
    {
        Task<Either<SMTA1WorkflowEmail, Errors>> Create(SMTA1WorkflowEmail SMTA1WorkflowEmail, CancellationToken cancellationToken);
        Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
        Task<SMTA1WorkflowEmail> ReadForUpdate(Guid id, CancellationToken cancellationToken);
        Task<Errors?> Update(SMTA1WorkflowEmail SMTA1WorkflowEmail, CancellationToken cancellationToken);
    }
}