using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.SMTA1WorkflowEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLSMTA1WorkflowEmailWriteRepository : ISMTA1WorkflowEmailWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<SMTA1WorkflowEmail> SMTA1WorkflowEmails => _dbContext.SMTA1WorkflowEmails;

    public SQLSMTA1WorkflowEmailWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<SMTA1WorkflowEmail, Errors>> Create(SMTA1WorkflowEmail SMTA1WorkflowEmail, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(SMTA1WorkflowEmail, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(SMTA1WorkflowEmail);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        SMTA1WorkflowEmail lab = await SMTA1WorkflowEmails.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        SMTA1WorkflowEmails.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<SMTA1WorkflowEmail> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.SMTA1WorkflowEmails
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(SMTA1WorkflowEmail SMTA1WorkflowEmail, CancellationToken cancellationToken)
    {
        SMTA1WorkflowEmails.Update(SMTA1WorkflowEmail);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}