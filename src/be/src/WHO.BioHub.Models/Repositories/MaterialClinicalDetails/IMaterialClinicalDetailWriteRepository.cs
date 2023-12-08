using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.MaterialClinicalDetails;

public interface IMaterialClinicalDetailWriteRepository
{
    Task<Either<MaterialClinicalDetail, Errors>> Create(MaterialClinicalDetail materialclinicaldetail, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<MaterialClinicalDetail> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(MaterialClinicalDetail materialclinicaldetail, CancellationToken cancellationToken);
}
