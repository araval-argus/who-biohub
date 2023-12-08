using WHO.BioHub.Shared.Utils;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.Models.Repositories.GeneticSequenceDatas;

public interface IGeneticSequenceDataWriteRepository
{
    Task<Either<GeneticSequenceData, Errors>> Create(GeneticSequenceData geneticsequencedata, CancellationToken cancellationToken);
    Task<Errors?> Delete(Guid id, CancellationToken cancellationToken);
    Task<GeneticSequenceData> ReadForUpdate(Guid id, CancellationToken cancellationToken);
    Task<Errors?> Update(GeneticSequenceData geneticsequencedata, CancellationToken cancellationToken);
}
