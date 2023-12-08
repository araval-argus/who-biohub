using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.GeneticSequenceDatas;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLGeneticSequenceDataWriteRepository : IGeneticSequenceDataWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<GeneticSequenceData> GeneticSequenceDatas => _dbContext.GeneticSequenceDatas;

    public SQLGeneticSequenceDataWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<GeneticSequenceData, Errors>> Create(GeneticSequenceData geneticsequencedata, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(geneticsequencedata, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(geneticsequencedata);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        GeneticSequenceData lab = await GeneticSequenceDatas.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        GeneticSequenceDatas.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<GeneticSequenceData> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.GeneticSequenceDatas
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(GeneticSequenceData geneticsequencedata, CancellationToken cancellationToken)
    {
        GeneticSequenceDatas.Update(geneticsequencedata);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}