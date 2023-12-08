using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLGeneticSequenceDataPublicReadRepository : IGeneticSequenceDataPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLGeneticSequenceDataPublicReadRepository(BioHubDbContext dbContext)
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