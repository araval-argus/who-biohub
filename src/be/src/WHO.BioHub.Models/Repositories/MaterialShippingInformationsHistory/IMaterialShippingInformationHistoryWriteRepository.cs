using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;

public interface IMaterialShippingInformationHistoryWriteRepository
{
    Task<Either<MaterialShippingInformationHistory, Errors>> Create(MaterialShippingInformationHistory materialshippinginformationhistory, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<MaterialShippingInformationHistory> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(MaterialShippingInformationHistory materialshippinginformationhistory, CancellationToken cancellationToken);
}
