using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialClinicalDetails;

public interface IMaterialClinicalDetailReadRepository
{
    Task<MaterialClinicalDetail> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<MaterialClinicalDetail>> List(CancellationToken cancellationToken);
}
