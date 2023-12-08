using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA2WorkflowEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA2WorkflowEmailWriteRepository : ISMTA2WorkflowEmailWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<SMTA2WorkflowEmail> SMTA2WorkflowEmails => _dbContext.SMTA2WorkflowEmails;

    public SQLSMTA2WorkflowEmailWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<SMTA2WorkflowEmail, Errors>> Create(SMTA2WorkflowEmail SMTA2WorkflowEmail, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(SMTA2WorkflowEmail, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(SMTA2WorkflowEmail);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        SMTA2WorkflowEmail lab = await SMTA2WorkflowEmails.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        SMTA2WorkflowEmails.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<SMTA2WorkflowEmail> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA2WorkflowEmails
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(SMTA2WorkflowEmail SMTA2WorkflowEmail, CancellationToken cancellationToken)
    {
        SMTA2WorkflowEmails.Update(SMTA2WorkflowEmail);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}