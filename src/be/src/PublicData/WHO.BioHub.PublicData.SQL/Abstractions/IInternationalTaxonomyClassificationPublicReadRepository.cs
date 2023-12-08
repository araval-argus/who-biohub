using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IInternationalTaxonomyClassificationPublicReadRepository
{
    Task<InternationalTaxonomyClassification> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<InternationalTaxonomyClassification>> List(CancellationToken cancellationToken);
}
