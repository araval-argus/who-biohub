using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;

public interface IWorklistToBioHubEmailWriteRepository
{
    Task<Either<WorklistToBioHubEmail, Errors>> Create(WorklistToBioHubEmail worklisttobiohubemail, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<WorklistToBioHubEmail> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(WorklistToBioHubEmail worklisttobiohubemail, CancellationToken cancellationToken);
}
