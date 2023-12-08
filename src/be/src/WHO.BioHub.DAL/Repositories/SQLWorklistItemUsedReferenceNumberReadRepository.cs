using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.WorklistFromBioHubItems;
using WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLWorklistItemUsedReferenceNumberReadRepository : IWorklistItemUsedReferenceNumberReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLWorklistItemUsedReferenceNumberReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> Count(bool? isPast, CancellationToken cancellationToken)
    {
        DateTime firstDateOfTheYear = new DateTime(DateTime.UtcNow.Year, 1, 1);

        var total = 0;

        if (isPast == true)
        {
            total = await _dbContext.WorklistItemUsedReferenceNumbers
            .CountAsync(l => l.CreationDate >= firstDateOfTheYear && l.IsPast == true, cancellationToken);
        }
        else
        {
            total = await _dbContext.WorklistItemUsedReferenceNumbers
            .CountAsync(l => l.CreationDate >= firstDateOfTheYear && l.IsPast != true, cancellationToken);
        }
        

        return total;
    }

    public async Task<bool> ReferenceNumberAlreadyPresent(string referenceNumber, CancellationToken cancellationToken)
    {
        return await _dbContext.WorklistItemUsedReferenceNumbers
            .AsNoTracking()
            .AnyAsync(l => l.DeletedOn == null && l.ReferenceNumber == referenceNumber, cancellationToken);
    }
}