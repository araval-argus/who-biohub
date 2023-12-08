using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BSLLevels;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBSLLevelWriteRepository : IBSLLevelWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<BSLLevel> BSLLevels => _dbContext.BSLLevels;

    public SQLBSLLevelWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<BSLLevel, Errors>> Create(BSLLevel bsllevel, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(bsllevel, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(bsllevel);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        BSLLevel lab = await BSLLevels.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        BSLLevels.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<BSLLevel> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BSLLevels
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(BSLLevel bsllevel, CancellationToken cancellationToken)
    {
        BSLLevels.Update(bsllevel);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}