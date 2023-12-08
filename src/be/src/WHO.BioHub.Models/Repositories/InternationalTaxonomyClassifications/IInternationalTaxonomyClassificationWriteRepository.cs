using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;

public interface IInternationalTaxonomyClassificationWriteRepository
{
    Task<Either<InternationalTaxonomyClassification, Errors>> Create(InternationalTaxonomyClassification internationaltaxonomyclassification, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<InternationalTaxonomyClassification> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(InternationalTaxonomyClassification internationaltaxonomyclassification, CancellationToken cancellationToken);
}
