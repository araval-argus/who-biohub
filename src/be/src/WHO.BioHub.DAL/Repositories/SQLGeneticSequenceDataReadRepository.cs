using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;

namespace WHO.BioHub.DAL.Repositories;

public class SQLGeneticSequenceDataReadRepository : IGeneticSequenceDataReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLGeneticSequenceDataReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<GeneticSequenceData>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.GeneticSequenceDatas
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<GeneticSequenceData> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.GeneticSequenceDatas
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }
}