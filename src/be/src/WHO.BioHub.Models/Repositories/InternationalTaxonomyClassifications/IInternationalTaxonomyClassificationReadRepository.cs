using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.InternationalTaxonomyClassifications;

public interface IInternationalTaxonomyClassificationReadRepository
{
    Task<InternationalTaxonomyClassification> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<InternationalTaxonomyClassification>> List(CancellationToken cancellationToken);
}
