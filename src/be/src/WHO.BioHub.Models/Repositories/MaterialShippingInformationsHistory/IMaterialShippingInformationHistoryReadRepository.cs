using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;

public interface IMaterialShippingInformationHistoryReadRepository
{
    Task<MaterialShippingInformationHistory> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialShippingInformationHistory>> List(CancellationToken cancellationToken);
}
