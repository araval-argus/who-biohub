using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialShippingInformations;

public interface IMaterialShippingInformationReadRepository
{
    Task<MaterialShippingInformation> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialShippingInformation>> List(CancellationToken cancellationToken);
}
