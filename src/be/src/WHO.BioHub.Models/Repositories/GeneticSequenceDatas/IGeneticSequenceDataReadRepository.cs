using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.GeneticSequenceDatas;

public interface IGeneticSequenceDataReadRepository
{
    Task<GeneticSequenceData> Read(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<GeneticSequenceData>> List(CancellationToken cancellationToken);
}
