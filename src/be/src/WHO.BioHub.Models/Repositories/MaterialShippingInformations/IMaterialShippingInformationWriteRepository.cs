using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialShippingInformations;

public interface IMaterialShippingInformationWriteRepository
{
    Task<Either<MaterialShippingInformation, Errors>> Create(MaterialShippingInformation materialshippinginformation, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<MaterialShippingInformation> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(MaterialShippingInformation materialshippinginformation, CancellationToken cancellationToken);
}
