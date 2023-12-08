using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Public.SQL.Abstractions;

public interface IGeneticSequenceDataPublicReadRepository
{
    Task<GeneticSequenceData> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<GeneticSequenceData>> List(CancellationToken cancellationToken);
}
