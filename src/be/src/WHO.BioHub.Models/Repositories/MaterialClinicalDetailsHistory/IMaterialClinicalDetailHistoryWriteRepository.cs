using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;

public interface IMaterialClinicalDetailHistoryWriteRepository
{
    Task<Either<MaterialClinicalDetailHistory, Errors>> Create(MaterialClinicalDetailHistory materialclinicaldetailhistory, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<MaterialClinicalDetailHistory> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(MaterialClinicalDetailHistory materialclinicaldetailhistory, CancellationToken cancellationToken);
}
