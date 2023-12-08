using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialClinicalDetailsHistory;

public interface IMaterialClinicalDetailHistoryReadRepository
{
    Task<MaterialClinicalDetailHistory> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialClinicalDetailHistory>> List(CancellationToken cancellationToken);
}
