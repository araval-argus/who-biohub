using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistToBioHubEmails;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistToBioHubEmailWriteRepository : IWorklistToBioHubEmailWriteRepository
{
    private readonly BioHubDbContext _dbContext;
    private DbSet<WorklistToBioHubEmail> WorklistToBioHubEmails => _dbContext.WorklistToBioHubEmails;

    public SQLWorklistToBioHubEmailWriteRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Either<WorklistToBioHubEmail, Errors>> Create(WorklistToBioHubEmail worklisttobiohubemail, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(worklisttobiohubemail, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return new(worklisttobiohubemail);
    }

    public async Task<Errors?> Delete(Guid id, CancellationToken cancellationToken)
    {
        WorklistToBioHubEmail lab = await WorklistToBioHubEmails.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
        lab.DeletedOn = DateTime.Now;
        WorklistToBioHubEmails.Update(lab);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return null;
    }

    public async Task<WorklistToBioHubEmail> ReadForUpdate(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistToBioHubEmails
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }

    public async Task<Errors?> Update(WorklistToBioHubEmail worklisttobiohubemail, CancellationToken cancellationToken)
    {
        WorklistToBioHubEmails.Update(worklisttobiohubemail);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return null;
    }
}